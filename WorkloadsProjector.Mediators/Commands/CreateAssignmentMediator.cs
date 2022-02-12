namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class CreateAssignmentMediator : IRequestHandler<CommandCreateAssignmentWithId, CommandAssignmentResponse>
    {
        private readonly ILogger<CreateAssignmentMediator> logger;

        public CreateAssignmentMediator(ILogger<CreateAssignmentMediator> logger) => this.logger = logger;

        public Task<CommandAssignmentResponse> Handle(CommandCreateAssignmentWithId request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandAssignmentResponse(request.AssignmentId, $"{request}"));
        }
    }

}