namespace NATS.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    using NATS.Client;
    //https://hub.docker.com/_/nats
    //https://docs.nats.io/running-a-nats-service/introduction/running/nats_docker/jetstream_docker
    //http://thinkmicroservices.com/blog/2021/jetstream/nats-jetstream.html

    //Ephemeral consumers are not long-lived.
    //By default, JetStream consumers are ephemeral.
    //Each connection appears to the server as a new consumer and will only track the consumer while active. 
    //Ephemeral consumers can only be push subscribers.

    //If the consumer is expected to be long-lived, we can create a durable consumer.
    //With a durable consumer, the server will keep track of the consumer's stream position.
    //If the consumer restarts, the server will continue at the last stream position.
    //Pull subscriptions require a durable consumer.
    //Its also important to understand that once a durable consumer has been created, some options cannot be changed.
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNatsClient(this IServiceCollection services, Action<Options>? configureOptions = null, ServiceLifetime connectionServiceLifeTime = ServiceLifetime.Transient)
        {
            Options? defaultOptions = ConnectionFactory.GetDefaultOptions();
            configureOptions?.Invoke(defaultOptions);
            services.AddSingleton(defaultOptions);

            services.AddSingleton<ConnectionFactory>();
            services.AddSingleton<INatsClientConnectionFactory, NatsClientConnectionFactory>();

            services.TryAdd(new ServiceDescriptor(typeof(IConnection), sp =>
            {
                Options? options = sp.GetRequiredService<Options>();
                INatsClientConnectionFactory? connectionFactory = sp.GetRequiredService<INatsClientConnectionFactory>();
                return connectionFactory.CreateConnection(options);
            }, connectionServiceLifeTime));

            services.TryAdd(new ServiceDescriptor(typeof(IEncodedConnection), sp =>
            {
                Options? options = sp.GetRequiredService<Options>();
                INatsClientConnectionFactory? connectionFactory = sp.GetRequiredService<INatsClientConnectionFactory>();
                return connectionFactory.CreateEncodedConnection(options);
            }, connectionServiceLifeTime));


            return services;
        }
    }
}