using System.Reflection;

using Common;
using Common.AspNet.Health;

using Countries.Db;
using Countries.Model;

using CountriesApi;

using MediatR;

using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ApplicationInfo appInfo = new(typeof(Program));
builder.Services.AddSingleton<ApplicationInfo>(appInfo);

builder.Services.AddHealth();
builder.Services.AddHostedService<Worker>();

// Add services to the container.
builder.Services.AddCountriesDb(builder.Configuration);

builder.Services.AddSingleton<PhonePrefixDictionary>();

builder.Services.AddMediatR(Assembly.GetAssembly(typeof(Program)) ?? throw new NullReferenceException());
builder.Services.Configure<ConnectionStrings>(c => builder.Configuration.GetSection("ConnectionStrings").Bind(c));

builder.Services.AddControllers();
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "Countries API",
    Version = $"v{appInfo.SemanticVersion}",
    Description = $"<i>Branch/Commit: {appInfo.Description}</i>"
}));

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy", policy =>
        policy.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()));
//          .AllowCredentials()) ;

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHealth();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
    c.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
    {
        ["activated"] = false
    };
});

//app.UseHttpsRedirection();

app.UseCors("Cors");

app.UseAuthorization();

app.MapControllers();

app.Run();
