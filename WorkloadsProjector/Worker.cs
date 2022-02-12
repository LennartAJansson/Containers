namespace WorkloadsProjector
{
    using System.Text;
    using System.Text.Json;

    using CloudNative.CloudEvents;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Client.JetStream;
    using NATS.Extensions.DependencyInjection;

    using Workloads.Contract;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly NatsConsumer natsConsumer;
        private readonly IConnection connection;
        private readonly IMediator mediator;
        private IJetStream? jetStream = null;
        private IJetStreamPushAsyncSubscription? subscription = null;

        public Worker(ILogger<Worker> logger, IOptions<NatsConsumer> options, IConnection connection, IMediator mediator)
        {
            this.logger = logger;
            natsConsumer = options.Value;
            this.connection = connection;
            this.mediator = mediator;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            JetStreamUtils.CreateStreamWhenDoesNotExist(connection, natsConsumer!.Stream!, natsConsumer!.Subject!);

            ConsumerConfiguration cc = ConsumerConfiguration.Builder()
                                    .WithDurable(natsConsumer.Consumer)
                                    .WithDeliverSubject(natsConsumer.DeliverySubject)
                                    .Build();

            connection.CreateJetStreamManagementContext()
                .AddOrUpdateConsumer(natsConsumer.Stream, cc);

            jetStream = connection.CreateJetStreamContext();

            PushSubscribeOptions so = PushSubscribeOptions.BindTo(natsConsumer.Stream, natsConsumer.Consumer);
            subscription = jetStream.PushSubscribeAsync(natsConsumer.Subject, MessageArrived, true, so);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time} - {server}", DateTimeOffset.Now, natsConsumer.Url);
                await Task.Delay(60000, stoppingToken);
            }
        }

        private void MessageArrived(object? sender, MsgHandlerEventArgs args)
        {
            Msg msg = args.Message;

            msg.Ack();

            CloudEvent? evt = JsonSerializer.Deserialize<CloudEvent>(Encoding.UTF8.GetString(msg!.Data!));

            switch (evt!.Type)
            {
                case "Workloads.Contract.CommandCreatePerson":
                    CommandCreatePerson? personToCreate = JsonSerializer.Deserialize<CommandCreatePerson>(evt.Data!.ToString()!);
                    mediator.Send(personToCreate!);
                    break;
                case "Workloads.Contract.CommandUpdatePerson":
                    CommandUpdatePerson? personToUpdate = JsonSerializer.Deserialize<CommandUpdatePerson>(evt.Data!.ToString()!);
                    mediator.Send(personToUpdate!);
                    break;
                case "Workloads.Contract.CommandDeletePerson":
                    CommandDeletePerson? personToDelete = JsonSerializer.Deserialize<CommandDeletePerson>(evt.Data!.ToString()!);
                    mediator.Send(personToDelete!);
                    break;
            }

            logger.LogInformation("Received message {data} on subject {subject}, stream {stream}, seqno {seqno}.",
                            Encoding.UTF8.GetString(msg.Data), natsConsumer.Subject, msg.MetaData.Stream, msg.MetaData.StreamSequence);
        }
    }
}