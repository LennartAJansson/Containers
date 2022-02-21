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

    public class CreatePersonMediator : ProjectorMediatorBase, IRequestHandler<CommandCreatePersonWithId, CommandPersonResponse>
    {
        private readonly ILogger<CreatePersonMediator> logger;

        public CreatePersonMediator(ILogger<CreatePersonMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public async Task<CommandPersonResponse> Handle(CommandCreatePersonWithId request, CancellationToken cancellationToken)
        {
            logger.LogDebug("{request}", request.ToString());

            Person person = await service.CreatePersonAsync(
                new Person
                {
                    PersonId = request.PersonId,
                    Name = request.Name
                });

            return new CommandPersonResponse(request.PersonId, $"{request} -> {JsonSerializer.Serialize(person)}");
        }
    }
}