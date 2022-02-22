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

    public class CreateWorkloadMediator : ProjectorMediatorBase, IRequestHandler<CommandCreateWorkloadWithId, CommandWorkloadResponse>
    {
        private readonly ILogger<CreateWorkloadMediator> logger;

        public CreateWorkloadMediator(ILogger<CreateWorkloadMediator> logger, IWorkloadsService service)
            : base(service) => this.logger = logger;

        public async Task<CommandWorkloadResponse> Handle(CommandCreateWorkloadWithId request, CancellationToken cancellationToken)
        {
            logger.LogDebug("{request}", request.ToString());

            Workload workload = await service.CreateWorkloadAsync(
                new Workload
                {
                    WorkloadId = request.WorkloadId,
                    Start = request.Start,
                    Stop = request.Stop,
                    AssignmentId = request.AssignmentId,
                    PersonId = request.PersonId
                });

            return new CommandWorkloadResponse(request.WorkloadId, $"{request} -> {JsonSerializer.Serialize(workload)}");
        }

    }
}
