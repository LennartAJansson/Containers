using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using NATS.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prometheus;

using Workloads.Model;

using WorkloadsApi.Health;
using WorkloadsApi.Mediators;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiMediators();
builder.Services.Configure<ConnectionStrings>(c => builder.Configuration.GetSection("ConnectionStrings").Bind(c));

builder.Services.AddHealthChecks().AddCheck<ApiHealthCheck>("Api Health Check").ForwardToPrometheus();

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

app.UseHealthChecks("/healthz", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResponseWriter = WriteResponse
});
app.UseMetricServer();
app.UseHttpMetrics();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static Task WriteResponse(HttpContext context, HealthReport report)
{
    context.Response.ContentType = "application/json";
    JObject? json = new JObject(
      new JProperty("status", report.Status.ToString()),
      new JProperty("results", new JObject(report.Entries.Select(pair =>
        new JProperty(pair.Key, new JObject(
          new JProperty("status", pair.Value.Status.ToString()),
          new JProperty("description", pair.Value.Description),
          new JProperty("data", new JObject(pair.Value.Data.Select(
            p => new JProperty(p.Key, p.Value)
                )))
            ))
        )))
    );
    return context.Response.WriteAsync(json.ToString(Formatting.Indented));
}
