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

    public class UpdateWorkloadMediator : IRequestHandler<CommandUpdateWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<UpdateWorkloadMediator> logger;
        private readonly IConnection connection;
        private readonly NatsProducer natsProducer;
        private readonly IJetStream? jetStream = null;

        public UpdateWorkloadMediator(ILogger<UpdateWorkloadMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        {
            this.logger = logger;
            connection = connection;
            natsProducer = options.Value;

            JetStreamUtils.CreateStreamOrUpdateSubjects(connection, natsProducer.Stream, natsProducer.Subject);
            jetStream = connection.CreateJetStreamContext();
        }

        public Task<CommandWorkloadResponse> Handle(CommandUpdateWorkload request, CancellationToken cancellationToken) =>
            Task.FromResult(new CommandWorkloadResponse(request.WorkloadId, $"{request.ToString()}"));
    }
}