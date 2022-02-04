using NATS.Extensions.DependencyInjection;

using WorkloadsApi.Mediators;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediators();

builder.Services.Configure<NatsProducer>(options => builder.Configuration.GetSection("NATS").Bind(options));
NatsProducer natsProducer = builder.Configuration.GetSection("NATS").Get<NatsProducer>();
builder.Services.AddNatsClient(options =>
{
    options.Servers = natsProducer.Servers;
    options.Url = natsProducer.Url;
    options.Verbose = true;
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
