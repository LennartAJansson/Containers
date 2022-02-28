using BuildVersion;

using Microsoft.EntityFrameworkCore;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MySqlServerVersion? serverVersion = new MySqlServerVersion(new Version(5, 6, 51));
builder.Services.AddDbContext<BuildVersionsDb>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("buildversionsdb"), serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors(),
            ServiceLifetime.Transient, ServiceLifetime.Transient);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();
app.Services.GetRequiredService<BuildVersionsDb>().EnsureDbExists();

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
