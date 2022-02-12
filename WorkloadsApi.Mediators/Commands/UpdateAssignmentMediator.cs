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

    public class UpdateAssignmentMediator : NatsCommandMediatorBase, IRequestHandler<CommandUpdateAssignment, CommandAssignmentResponse>
    {
        private readonly ILogger<UpdateAssignmentMediator> logger;

        public UpdateAssignmentMediator(ILogger<UpdateAssignmentMediator> logger, IConnection connection, IOptions<NatsProducer> options)
            : base(connection, options.Value)
            => this.logger = logger;

        public Task<CommandAssignmentResponse> Handle(CommandUpdateAssignment request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            return Task.FromResult(new CommandAssignmentResponse(request.AssignmentId, $"{request}"));
        }
    }
}