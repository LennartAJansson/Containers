namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class CreateWorkloadMediator : IRequestHandler<CommandCreateWorkloadWithId, CommandWorkloadResponse>
    {
        private readonly ILogger<CreateWorkloadMediator> logger;

        public CreateWorkloadMediator(ILogger<CreateWorkloadMediator> logger) => this.logger = logger;

        public Task<CommandWorkloadResponse> Handle(CommandCreateWorkloadWithId request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{request}", request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandWorkloadResponse(request.WorkloadId, $"{request}"));
        }
    }

}