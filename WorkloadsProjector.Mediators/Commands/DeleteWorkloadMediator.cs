namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class DeleteWorkloadMediator : IRequestHandler<CommandDeleteWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<DeleteWorkloadMediator> logger;

        public DeleteWorkloadMediator(ILogger<DeleteWorkloadMediator> logger) => this.logger = logger;
        public Task<CommandWorkloadResponse> Handle(CommandDeleteWorkload request, CancellationToken cancellationToken)
        {
            logger.LogInformation(request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandWorkloadResponse(request.WorkloadId, $"{request.ToString()}"));
        }
    }

}