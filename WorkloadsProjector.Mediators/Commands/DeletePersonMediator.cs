namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class DeletePersonMediator : IRequestHandler<CommandDeletePerson, CommandPersonResponse>
    {
        private readonly ILogger<DeletePersonMediator> logger;

        public DeletePersonMediator(ILogger<DeletePersonMediator> logger) => this.logger = logger;
        public Task<CommandPersonResponse> Handle(CommandDeletePerson request, CancellationToken cancellationToken)
        {
            logger.LogInformation(request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandPersonResponse(request.PersonId, $"{request.ToString()}"));
        }
    }

}