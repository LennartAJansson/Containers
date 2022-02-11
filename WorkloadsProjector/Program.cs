
using NATS.Extensions.DependencyInjection;

using WorkloadsProjector;
using WorkloadsProjector.Mediators;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddProjectorMediators();

        services.Configure<NatsConsumer>(options => context.Configuration.GetSection("NATS").Bind(options));
        NatsConsumer natsConsumer = context.Configuration.GetSection("NATS").Get<NatsConsumer>();
        services.AddNatsClient(options =>
        {
            options.Servers = natsConsumer.Servers;
            options.Url = natsConsumer.Url;
            options.Verbose = true;
        });

        services.AddHostedService<Worker>();
        services.AddHostedService<HttpHealthcheck>();
    })
    //.ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>())
    .Build();

await host.RunAsync();
