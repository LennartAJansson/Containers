namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;
    using Workloads.Db;

    public class UpdateWorkloadMediator : ProjectorMediatorBase, IRequestHandler<CommandUpdateWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<UpdateWorkloadMediator> logger;

        public UpdateWorkloadMediator(ILogger<UpdateWorkloadMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public Task<CommandWorkloadResponse> Handle(CommandUpdateWorkload request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandWorkloadResponse(request.WorkloadId, $"{request}"));
        }
    }
}