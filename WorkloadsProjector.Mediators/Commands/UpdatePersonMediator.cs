namespace WorkloadsProjector.Mediators.Commands
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;
    using Workloads.Db;
    using Workloads.Model;

    public class UpdatePersonMediator : ProjectorMediatorBase, IRequestHandler<CommandUpdatePerson, CommandPersonResponse>
    {
        private readonly ILogger<UpdatePersonMediator> logger;

        public UpdatePersonMediator(ILogger<UpdatePersonMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public async Task<CommandPersonResponse> Handle(CommandUpdatePerson request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());

            Person person = await service.UpdatePersonAsync(new Person
            {
                PersonId = request.PersonId,
                Name = request.Name
            });

            return new CommandPersonResponse(request.PersonId, $"{request} -> {JsonSerializer.Serialize(person)}");
        }
    }
}