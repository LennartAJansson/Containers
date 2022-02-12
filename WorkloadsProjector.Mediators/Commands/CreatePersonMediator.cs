namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class CreatePersonMediator : IRequestHandler<CommandCreatePersonWithId, CommandPersonResponse>
    {
        private readonly ILogger<CreatePersonMediator> logger;

        public CreatePersonMediator(ILogger<CreatePersonMediator> logger) => this.logger = logger;

        public Task<CommandPersonResponse> Handle(CommandCreatePersonWithId request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandPersonResponse(request.PersonId, $"{request}"));
        }
    }
}