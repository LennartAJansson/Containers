namespace WorkloadsApi.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Client.JetStream;
    using NATS.Extensions.DependencyInjection;

    using Workloads.Contract;

    public class DeletePersonMediator : IRequestHandler<CommandDeletePerson, CommandPersonResponse>
    {
        private readonly ILogger<DeletePersonMediator> logger;
        private readonly IConnection connection;
        private readonly NatsProducer natsProducer;
        private readonly IJetStream? jetStream = null;

        public DeletePersonMediator(ILogger<DeletePersonMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        {
            this.logger = logger;
            connection = connection;
            natsProducer = options.Value;

            JetStreamUtils.CreateStreamOrUpdateSubjects(connection, natsProducer.Stream, natsProducer.Subject);
            jetStream = connection.CreateJetStreamContext();
        }
        public Task<CommandPersonResponse> Handle(CommandDeletePerson request, CancellationToken cancellationToken) =>
            Task.FromResult(new CommandPersonResponse(request.PersonId, $"{request.ToString()}"));
    }

}