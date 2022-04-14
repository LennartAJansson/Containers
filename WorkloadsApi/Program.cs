using Common;

using Microsoft.OpenApi.Models;

using NATS.Extensions.DependencyInjection;

using Prometheus;

using Workloads.Model;

using WorkloadsApi.Health;
using WorkloadsApi.Mediators;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

ApplicationInfo appInfo = new ApplicationInfo(typeof(Program));
builder.Services.AddSingleton<ApplicationInfo>(appInfo);

builder.Services.AddHealth();

// Add services to the container.
builder.Services.AddHostedService<Worker>();
builder.Services.AddApiMediators();
builder.Services.Configure<ConnectionStrings>(c => builder.Configuration.GetSection("ConnectionStrings").Bind(c));

builder.Services.Configure<NatsProducer>(options => builder.Configuration.GetSection("NATS").Bind(options));
builder.Services.AddNatsClient(options =>
{
    NatsProducer natsProducer = builder.Configuration.GetSection("NATS").Get<NatsProducer>();
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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Workloads API",
        Version = $"v{appInfo.SemanticVersion}",
        Description = $"<i>Branch/Commit: {appInfo.Description}</i>"
    });
    string xmlFile = $"{appInfo.ExecutingAssembly.GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, true);
});

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHealth();

app.UseMetricServer();
app.UseHttpMetrics();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
