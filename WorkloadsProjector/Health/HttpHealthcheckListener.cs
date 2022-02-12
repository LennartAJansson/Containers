namespace WorkloadsProjector
{
    using System.Net;
    using System.Text;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class HttpHealthcheckListener : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly HttpListener httpListener;
        private readonly HealthConfiguration healthConfiguration;


        public HttpHealthcheckListener(ILogger<Worker> logger, IOptions<HealthConfiguration> options)
        {
            this.logger = logger;
            healthConfiguration = options.Value;
            httpListener = new HttpListener();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            httpListener.Prefixes.Add(healthConfiguration.ProbeUrl);

            httpListener.Start();
            logger.LogInformation($"Healthcheck listening...");

            while (!stoppingToken.IsCancellationRequested)
            {
                HttpListenerContext? ctx = null;
                try
                {
                    ctx = await httpListener.GetContextAsync();
                }
                catch (HttpListenerException ex)
                {
                    if (ex.ErrorCode == 995)
                    {
                        return;
                    }
                }

                if (ctx == null)
                {
                    continue;
                }

                HttpListenerResponse? response = ctx.Response;
                response.ContentType = "text/plain";
                response.Headers.Add(HttpResponseHeader.CacheControl, "no-store, no-cache");
                response.StatusCode = (int)HttpStatusCode.OK;

                byte[]? messageBytes = Encoding.UTF8.GetBytes("Healthy");
                response.ContentLength64 = messageBytes.Length;
                await response.OutputStream.WriteAsync(messageBytes, stoppingToken);
                response.OutputStream.Close();
                response.Close();
            }
        }
    }


    public class HealthConfiguration
    {
        public string ProbeUrl { get; set; }
    }

}