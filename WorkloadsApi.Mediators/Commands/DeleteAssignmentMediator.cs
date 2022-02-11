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

    public class DeleteAssignmentMediator : IRequestHandler<CommandDeleteAssignment, CommandAssignmentResponse>
    {
        private readonly ILogger<DeleteAssignmentMediator> logger;
        private readonly IConnection connection;
        private readonly NatsProducer natsProducer;
        private readonly IJetStream? jetStream = null;

        public DeleteAssignmentMediator(ILogger<DeleteAssignmentMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        {
            this.logger = logger;
            connection = connection;
            natsProducer = options.Value;

            JetStreamUtils.CreateStreamOrUpdateSubjects(connection, natsProducer.Stream, natsProducer.Subject);
            jetStream = connection.CreateJetStreamContext();
        }
        public Task<CommandAssignmentResponse> Handle(CommandDeleteAssignment request, CancellationToken cancellationToken) =>
            Task.FromResult(new CommandAssignmentResponse(request.AssignmentId, $"{request.ToString()}"));
    }
}