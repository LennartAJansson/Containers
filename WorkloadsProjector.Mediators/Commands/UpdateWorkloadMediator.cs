namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class UpdateWorkloadMediator : IRequestHandler<CommandUpdateWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<UpdateWorkloadMediator> logger;

        public UpdateWorkloadMediator(ILogger<UpdateWorkloadMediator> logger) => this.logger = logger;

        public Task<CommandWorkloadResponse> Handle(CommandUpdateWorkload request, CancellationToken cancellationToken)
        {
            logger.LogInformation(request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandWorkloadResponse(request.WorkloadId, $"{request.ToString()}"));
        }
    }
}