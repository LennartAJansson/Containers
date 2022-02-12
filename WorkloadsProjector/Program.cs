
using NATS.Extensions.DependencyInjection;

using WorkloadsProjector;
using WorkloadsProjector.Mediators;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<HealthConfiguration>(h => context.Configuration.GetSection("Health").Bind(h));
        services.AddHostedService<HttpHealthcheckListener>();

        services.AddProjectorMediators();
        services.AddWorkloadsDb(context.Configuration);

        services.Configure<NatsConsumer>(options => context.Configuration.GetSection("NATS").Bind(options));
        NatsConsumer natsConsumer = context.Configuration.GetSection("NATS").Get<NatsConsumer>();
        services.AddNatsClient(options =>
        {
            options.Servers = natsConsumer.Servers;
            options.Url = natsConsumer.Url;
            options.Verbose = true;
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.UpdateDb().RunAsync();
