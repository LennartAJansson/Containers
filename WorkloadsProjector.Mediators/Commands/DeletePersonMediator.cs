namespace WorkloadsProjector.Mediators.Commands
{
    using MediatR;

    using Microsoft.Extensions.Logging;

    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Workloads.Contract;
    using Workloads.Db;
    using Workloads.Model;

    public class DeletePersonMediator : ProjectorMediatorBase, IRequestHandler<CommandDeletePerson, CommandPersonResponse>
    {
        private readonly ILogger<DeletePersonMediator> logger;

        public DeletePersonMediator(ILogger<DeletePersonMediator> logger, IWorkloadsService service)
            : base(service)
        {
            this.logger = logger;
        }

        public async Task<CommandPersonResponse> Handle(CommandDeletePerson request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());

            Person person = await service.DeletePersonAsync(request.PersonId);

            return new CommandPersonResponse(request.PersonId, $"{request} -> {JsonSerializer.Serialize(person)}");
        }
    }
}