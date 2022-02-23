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

    public class DeleteAssignmentMediator : ProjectorMediatorBase, IRequestHandler<CommandDeleteAssignment, CommandAssignmentResponse>
    {
        private readonly ILogger<DeleteAssignmentMediator> logger;

        public DeleteAssignmentMediator(ILogger<DeleteAssignmentMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public async Task<CommandAssignmentResponse> Handle(CommandDeleteAssignment request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());

            Assignment assignment = await service.DeleteAssignmentAsync(request.AssignmentId);

            return new CommandAssignmentResponse(request.AssignmentId, $"{request} -> {JsonSerializer.Serialize(assignment)}");
        }
    }

}