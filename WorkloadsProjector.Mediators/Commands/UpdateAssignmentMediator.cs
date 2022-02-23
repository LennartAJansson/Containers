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

    public class UpdateAssignmentMediator : ProjectorMediatorBase, IRequestHandler<CommandUpdateAssignment, CommandAssignmentResponse>
    {
        private readonly ILogger<UpdateAssignmentMediator> logger;

        public UpdateAssignmentMediator(ILogger<UpdateAssignmentMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public async Task<CommandAssignmentResponse> Handle(CommandUpdateAssignment request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());

            Assignment assignment = await service.UpdateAssignmentAsync(new Assignment
            {
                AssignmentId = request.AssignmentId,
                CustomerName = request.Customer,
                Description = request.Description
            });

            return new CommandAssignmentResponse(request.AssignmentId, $"{request} -> {JsonSerializer.Serialize(assignment)}");
        }
    }
}