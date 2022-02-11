namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class DeleteAssignmentMediator : IRequestHandler<CommandDeleteAssignment, CommandAssignmentResponse>
    {
        private readonly ILogger<DeleteAssignmentMediator> logger;

        public DeleteAssignmentMediator(ILogger<DeleteAssignmentMediator> logger) => this.logger = logger;
        public Task<CommandAssignmentResponse> Handle(CommandDeleteAssignment request, CancellationToken cancellationToken)
        {
            logger.LogInformation(request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandAssignmentResponse(request.AssignmentId, $"{request.ToString()}"));
        }
    }

}