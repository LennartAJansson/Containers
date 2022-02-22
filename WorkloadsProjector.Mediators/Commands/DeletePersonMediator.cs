namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;
    using Workloads.Db;

    public class DeletePersonMediator : ProjectorMediatorBase, IRequestHandler<CommandDeletePerson, CommandPersonResponse>
    {
        private readonly ILogger<DeletePersonMediator> logger;

        public DeletePersonMediator(ILogger<DeletePersonMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public Task<CommandPersonResponse> Handle(CommandDeletePerson request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandPersonResponse(request.PersonId, $"{request}"));
        }
    }
}