namespace CountriesApi.Health
{

    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    //using Prometheus;

    public static class HealthExtensions
    {
        public static IServiceCollection AddHealth(this IServiceCollection services)
        {
            services.AddSingleton(sp => new WorkerWitness());
            services.AddHealthChecks().AddCheck<ApiHealthCheck>("Api Health Check");//.ForwardToPrometheus();

            return services;
        }

        public static WebApplication UseHealth(this WebApplication app)
        {
            app.UseHealthChecks("/healthz", new HealthCheckOptions
            {
                AllowCachingResponses = false,
                ResponseWriter = WriteResponse
            });

            return app;
        }

        private static Task WriteResponse(HttpContext context, HealthReport report)
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