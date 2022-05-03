
using BuildVersionsApi.Data;

using Common;
using Common.AspNet.Health;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ApplicationInfo appInfo = new(typeof(Program));
builder.Services.AddSingleton<ApplicationInfo>(appInfo);

builder.Services.AddHealth();
builder.Services.AddHostedService<Worker>();

MySqlServerVersion serverVersion = new(new Version(5, 6, 51));
builder.Services.AddDbContext<BuildVersionsDb>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("buildversionsdb"), serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors(),
            ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "BuildVersionsApi",
    Version = $"v{appInfo.SemanticVersion}",
    Description = $"<i>Branch/Commit: {appInfo.Description}</i>"
}));

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
      builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()));

WebApplication app = builder.Build();

app.Services.GetRequiredService<BuildVersionsDb>().EnsureDbExists();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseCors("Cors");

app.UseHealth();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
