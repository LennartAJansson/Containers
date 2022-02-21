namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;
    using Workloads.Db;

    public class DeleteAssignmentMediator : ProjectorMediatorBase, IRequestHandler<CommandDeleteAssignment, CommandAssignmentResponse>
    {
        private readonly ILogger<DeleteAssignmentMediator> logger;

        public DeleteAssignmentMediator(ILogger<DeleteAssignmentMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public Task<CommandAssignmentResponse> Handle(CommandDeleteAssignment request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandAssignmentResponse(request.AssignmentId, $"{request}"));
        }
    }

}