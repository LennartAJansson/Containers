namespace WorkloadsProjector
{
    using CloudNative.CloudEvents;

    using Common.Health;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Client.JetStream;
    using NATS.Extensions.DependencyInjection;

    using Prometheus;

    using System.Text;
    using System.Text.Json;

    using Workloads.Contract;

    public class Worker : BackgroundService
    {
        private static Gauge requestExecuteTime = Metrics.CreateGauge("workloadsprojector_executiontime", "Counts total execution time for handling requests",
            new GaugeConfiguration
            {
                LabelNames = new[] { "type" }
            });

        private static Counter counter = Metrics.CreateCounter("workloadprojector_counter", "Counts total calls for handling requests",
            new CounterConfiguration
            {
                LabelNames = new[] { "type" }
            });
        protected static Gauge RequestExecuteTime { get => requestExecuteTime; set => requestExecuteTime = value; }
        protected static Counter Counter { get => counter; set => counter = value; }

        private readonly ILogger<Worker> logger;
        private readonly NatsConsumer natsConsumer;
        private readonly IConnection connection;
        private readonly IMediator mediator;
        private readonly WorkerWitness witness;
        private IJetStream jetStream = null;
        private IJetStreamPushAsyncSubscription subscription = null;

        public Worker(ILogger<Worker> logger, IOptions<NatsConsumer> options, IConnection connection, IMediator mediator, WorkerWitness witness)
        {
            this.logger = logger;
            natsConsumer = options.Value;
            this.connection = connection;
            this.mediator = mediator;
            this.witness = witness;
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
                witness.LastExecution = DateTime.Now;
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void MessageArrived(object sender, MsgHandlerEventArgs args)
        {
            DateTime startDateTime = DateTime.Now;

            Msg msg = args.Message;

            msg.Ack();

            CloudEvent evt = JsonSerializer.Deserialize<CloudEvent>(Encoding.UTF8.GetString(msg!.Data!));

            logger.LogDebug("Received message {data} on subject {subject}, stream {stream}, seqno {seqno}.",
                            Encoding.UTF8.GetString(msg.Data), natsConsumer.Subject, msg.MetaData.Stream, msg.MetaData.StreamSequence);

            string status = "";

            switch (evt!.Type)
            {
                case "Workloads.Contract.CommandCreatePersonWithId":
                    CommandCreatePersonWithId personToCreate = JsonSerializer.Deserialize<CommandCreatePersonWithId>(evt.Data!.ToString()!);
                    status = JsonSerializer.Serialize(mediator.Send(personToCreate!));
                    break;
                case "Workloads.Contract.CommandUpdatePerson":
                    CommandUpdatePerson personToUpdate = JsonSerializer.Deserialize<CommandUpdatePerson>(evt.Data!.ToString()!);
                    status = JsonSerializer.Serialize(mediator.Send(personToUpdate!));
                    break;
                case "Workloads.Contract.CommandDeletePerson":
                    CommandDeletePerson personToDelete = JsonSerializer.Deserialize<CommandDeletePerson>(evt.Data!.ToString()!);
                    status = JsonSerializer.Serialize(mediator.Send(personToDelete!));
                    break;
                case "Workloads.Contract.CommandCreateAssignmentWithId":
                    CommandCreateAssignmentWithId assignmentToCreate = JsonSerializer.Deserialize<CommandCreateAssignmentWithId>(evt.Data!.ToString()!);
                    status = JsonSerializer.Serialize(mediator.Send(assignmentToCreate!));
                    break;
                case "Workloads.Contract.CommandUpdateAssignment":
                    CommandUpdateAssignment assignmentToUpdate = JsonSerializer.Deserialize<CommandUpdateAssignment>(evt.Data!.ToString()!);
                    mediator.Send(assignmentToUpdate!);
                    status = JsonSerializer.Serialize(mediator.Send(assignmentToUpdate!));
                    break;
                case "Workloads.Contract.CommandDeleteAssignment":
                    CommandDeleteAssignment assignmentToDelete = JsonSerializer.Deserialize<CommandDeleteAssignment>(evt.Data!.ToString()!);
                    status = JsonSerializer.Serialize(mediator.Send(assignmentToDelete!));
                    break;
                case "Workloads.Contract.CommandCreateWorkloadWithId":
                    CommandCreateWorkloadWithId workloadToCreate = JsonSerializer.Deserialize<CommandCreateWorkloadWithId>(evt.Data!.ToString()!);
                    status = JsonSerializer.Serialize(mediator.Send(workloadToCreate!));
                    break;
                case "Workloads.Contract.CommandUpdateWorkload":
                    CommandUpdateWorkload workloadToUpdate = JsonSerializer.Deserialize<CommandUpdateWorkload>(evt.Data!.ToString()!);
                    status = JsonSerializer.Serialize(mediator.Send(workloadToUpdate!));
                    break;
                case "Workloads.Contract.CommandDeleteWorkload":
                    CommandDeleteWorkload workloadToDelete = JsonSerializer.Deserialize<CommandDeleteWorkload>(evt.Data!.ToString()!);
                    status = JsonSerializer.Serialize(mediator.Send(workloadToDelete!));
                    break;
            }
            logger.LogDebug("{text}", status);
            DateTime endDateTime = DateTime.Now;
            string label = (evt != null && evt.Type != null) ? evt.Type : "unknown";
            RequestExecuteTime.Labels(label).Set((endDateTime - startDateTime).TotalMilliseconds);
            Counter.Labels(label).Inc();
        }
    }
}