namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class UpdateAssignmentMediator : IRequestHandler<CommandUpdateAssignment, CommandAssignmentResponse>
    {
        private readonly ILogger<UpdateAssignmentMediator> logger;

        public UpdateAssignmentMediator(ILogger<UpdateAssignmentMediator> logger) => this.logger = logger;

        public Task<CommandAssignmentResponse> Handle(CommandUpdateAssignment request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandAssignmentResponse(request.AssignmentId, $"{request}"));
        }
    }
}