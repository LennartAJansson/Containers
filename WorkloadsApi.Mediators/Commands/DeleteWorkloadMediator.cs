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

    public class DeleteWorkloadMediator : IRequestHandler<CommandDeleteWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<DeleteWorkloadMediator> logger;
        private readonly IConnection connection;
        private readonly NatsProducer natsProducer;
        private readonly IJetStream? jetStream = null;

        public DeleteWorkloadMediator(ILogger<DeleteWorkloadMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        {
            this.logger = logger;
            connection = connection;
            natsProducer = options.Value;

            JetStreamUtils.CreateStreamOrUpdateSubjects(connection, natsProducer.Stream, natsProducer.Subject);
            jetStream = connection.CreateJetStreamContext();
        }

        public Task<CommandWorkloadResponse> Handle(CommandDeleteWorkload request, CancellationToken cancellationToken) =>
            Task.FromResult(new CommandWorkloadResponse(request.WorkloadId, $"{request.ToString()}"));
    }
}