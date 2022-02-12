namespace WorkloadsProjector
{
    using System.Net;
    using System.Text;

    using Microsoft.Extensions.Logging;

    public class HttpHealthcheck : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly HttpListener httpListener;
        //private readonly IConfiguration configuration;


        public HttpHealthcheck(ILogger<Worker> logger/*, IConfiguration configuration*/)
        {
            this.logger = logger;
            //this.configuration = configuration;
            httpListener = new HttpListener();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            httpListener.Prefixes.Add($"http://*:5001/healthz/live/");
            httpListener.Prefixes.Add($"http://*:5001/healthz/ready/");

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
}