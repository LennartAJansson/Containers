using Common;
using Common.AspNet.Health;

using NATS.Extensions.DependencyInjection;

using WorkloadsProjector;
using WorkloadsProjector.Mediators;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        ApplicationInfo appInfo = new ApplicationInfo(typeof(Program));
        services.AddSingleton<ApplicationInfo>(appInfo);

        services.AddProjectorMediators();
        services.AddWorkloadsDb(context.Configuration);
        services.AddHealth();
        services.Configure<NatsConsumer>(options => context.Configuration.GetSection("NATS").Bind(options));
        services.AddNatsClient(options =>
        {
            NatsConsumer natsConsumer = context.Configuration.GetSection("NATS").Get<NatsConsumer>();
            options.Servers = natsConsumer.Servers;
            options.Url = natsConsumer.Url;
            options.Verbose = true;
        });

        services.AddHostedService<Worker>();
    })
    .UseHealth()
    .Build();

await host.UpdateDb().RunAsync();

