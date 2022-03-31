using BuildVersion;

using Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

ApplicationInfo appInfo = new ApplicationInfo(typeof(Program));
builder.Services.AddSingleton<ApplicationInfo>(appInfo);

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BuildVersion API",
        Version = $"v{appInfo.SemanticVersion}",
        Description = $"<i>Branch/Commit: {appInfo.Description}</i>"
    });
    string xmlFile = $"{appInfo.ExecutingAssembly.GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, true);
});

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
