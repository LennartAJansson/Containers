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

    public class UpdateWorkloadMediator : ProjectorMediatorBase, IRequestHandler<CommandUpdateWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<UpdateWorkloadMediator> logger;

        public UpdateWorkloadMediator(ILogger<UpdateWorkloadMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public async Task<CommandWorkloadResponse> Handle(CommandUpdateWorkload request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());

            Workload workload = await service.UpdateWorkloadAsync(new Workload
            {
                WorkloadId = request.WorkloadId,
                AssignmentId = request.AssignmentId,
                PersonId = request.PersonId,
                Start = request.Start,
                Stop = request.Stop
            });

            return new CommandWorkloadResponse(request.WorkloadId, $"{request} -> {JsonSerializer.Serialize(workload)}");

        }
    }
}