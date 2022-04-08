namespace CronJob;

using Common;

using Countries.Db;

using CronJob.Client;

using Refit;

internal class Program
{
    private static IHost? host;

    private static async Task Main(string[] args)
    {
        host = CreateHostBuilder(args).Build();

        await host.StartAsync();

        using IServiceScope? scope = host.Services.CreateScope();

        host.UpdateDb();

        IServiceProvider? provider = scope.ServiceProvider;
        ILogger<Program>? logger = provider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("This is my CronJob!");
        await provider.GetRequiredService<Worker>().ExecuteAsync();

        await host.StopAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, services) =>
        {
            services.AddCountriesDb(context.Configuration);
            services.AddSingleton<Worker>();
            services.AddSingleton(new ApplicationInfo(typeof(Program)));
            services.AddRefitClient<ICountryApiClient>()
                .ConfigureHttpClient(client =>
                    client.BaseAddress = new Uri(context.Configuration.GetConnectionString("CountryApiUrl")));
        });
    }
}
