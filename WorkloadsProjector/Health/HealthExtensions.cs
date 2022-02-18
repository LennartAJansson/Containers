namespace WorkloadsProjector.Health
{
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class HealthExtensions
    {
        public static IHostBuilder UseHealth(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureWebHostDefaults(builder =>
            {
                builder.Configure(app =>
                {
                    app.UseRouting().UseEndpoints(config =>
                    {
                        config.MapHealthChecks("/healthz", new HealthCheckOptions
                        {
                            AllowCachingResponses = false,
                            ResponseWriter = WriteResponse
                        });
                    });
                });
            });

            return hostBuilder;
        }

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
    }
}