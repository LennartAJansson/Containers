namespace WorkloadsApi.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Extensions.DependencyInjection;

    using Workloads.Contract;

    public class DeleteWorkloadMediator : NatsCommandMediatorBase, IRequestHandler<CommandDeleteWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<DeleteWorkloadMediator> logger;

        public DeleteWorkloadMediator(ILogger<DeleteWorkloadMediator> logger, IConnection connection, IOptions<NatsProducer> options)
            : base(connection, options.Value)
            => this.logger = logger;

        public Task<CommandWorkloadResponse> Handle(CommandDeleteWorkload request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            return Task.FromResult(new CommandWorkloadResponse(request.WorkloadId, $"{request}"));
        }
    }
}