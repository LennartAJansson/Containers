namespace WorkloadsApi.Mediators.Commands
{

    using NATS.Client;
    using NATS.Client.JetStream;
    using NATS.Extensions.DependencyInjection;

    public class NatsCommandMediatorBase
    {
        protected readonly IConnection connection;
        protected readonly NatsProducer natsProducer;
        protected readonly IJetStream jetStream = null;
        public NatsCommandMediatorBase(IConnection connection, NatsProducer natsProducer)
        {
            this.connection = connection;
            this.natsProducer = natsProducer;

            JetStreamUtils.CreateStreamOrUpdateSubjects(this.connection, this.natsProducer!.Stream!, this.natsProducer!.Subject!);
            jetStream = connection.CreateJetStreamContext();
        }
    }

}