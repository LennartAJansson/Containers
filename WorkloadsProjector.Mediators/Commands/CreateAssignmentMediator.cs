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

    public class CreateAssignmentMediator : ProjectorMediatorBase, IRequestHandler<CommandCreateAssignmentWithId, CommandAssignmentResponse>
    {
        private readonly ILogger<CreateAssignmentMediator> logger;

        public CreateAssignmentMediator(ILogger<CreateAssignmentMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public async Task<CommandAssignmentResponse> Handle(CommandCreateAssignmentWithId request, CancellationToken cancellationToken)
        {
            logger.LogDebug("{request}", request.ToString());

            Assignment assignment = await service.CreateAssignmentAsync(
                new Assignment
                {
                    AssignmentId = request.AssignmentId,
                    CustomerName = request.CustomerName,
                    Description = request.Description
                });

            return new CommandAssignmentResponse(request.AssignmentId, $"{request} -> {JsonSerializer.Serialize(assignment)}");
        }
    }

}