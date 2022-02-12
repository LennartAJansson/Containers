namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;
    public class UpdatePersonMediator : IRequestHandler<CommandUpdatePerson, CommandPersonResponse>
    {
        private readonly ILogger<UpdatePersonMediator> logger;

        public UpdatePersonMediator(ILogger<UpdatePersonMediator> logger) => this.logger = logger;

        public Task<CommandPersonResponse> Handle(CommandUpdatePerson request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandPersonResponse(request.PersonId, $"{request}"));
        }
    }
}