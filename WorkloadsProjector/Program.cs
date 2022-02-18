
using NATS.Extensions.DependencyInjection;

using WorkloadsProjector;
using WorkloadsProjector.Health;
using WorkloadsProjector.Mediators;

WorkerWitness? witness = new WorkerWitness();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(sp => witness);
        services.AddHealthChecks().AddCheck<ProjectorHealthCheck>("workloadsprojector");

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
    .UseHealth()
    .Build();

await host.UpdateDb().RunAsync();

