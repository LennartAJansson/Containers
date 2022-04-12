using Common;

using Countries.Db;
using Countries.Model;

using CountriesApi;

using MediatR;

using Microsoft.OpenApi.Models;

using System.Reflection;
using System.Text.Json.Serialization;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ApplicationInfo appInfo = new ApplicationInfo(typeof(Program));
builder.Services.AddCountriesDb(builder.Configuration);
builder.Services.AddSingleton<ApplicationInfo>(appInfo);

builder.Services.AddSingleton<CountryDictionary>();

builder.Services.AddMediatR(Assembly.GetAssembly(typeof(Program)) ?? throw new NullReferenceException());
builder.Services.Configure<ConnectionStrings>(c => builder.Configuration.GetSection("ConnectionStrings").Bind(c));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Countries API",
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

app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Countries API");
    //    c.RoutePrefix = string.Empty;
    c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
    c.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
    {
        ["activated"] = false
    };
});

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();