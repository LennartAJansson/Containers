namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class CreatePersonMediator : IRequestHandler<CommandCreatePerson, CommandPersonResponse>
    {
        private readonly ILogger<CreatePersonMediator> logger;

        public CreatePersonMediator(ILogger<CreatePersonMediator> logger) => this.logger = logger;

        public Task<CommandPersonResponse> Handle(CommandCreatePerson request, CancellationToken cancellationToken)
        {
            logger.LogInformation(request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandPersonResponse(1, $"{request}"));
        }
    }

}