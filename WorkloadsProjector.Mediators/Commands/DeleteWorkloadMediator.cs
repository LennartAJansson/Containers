namespace WorkloadsProjector.Mediators.Commands
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;
    using Workloads.Db;

    public class DeleteWorkloadMediator : ProjectorMediatorBase, IRequestHandler<CommandDeleteWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<DeleteWorkloadMediator> logger;

        public DeleteWorkloadMediator(ILogger<DeleteWorkloadMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public async Task<CommandWorkloadResponse> Handle(CommandDeleteWorkload request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());

            Workloads.Model.Workload? workload = await service.DeleteWorkloadAsync(request.WorkloadId);

            return new CommandWorkloadResponse(request.WorkloadId, $"{request} -> {JsonSerializer.Serialize(workload)}");
        }
    }

}