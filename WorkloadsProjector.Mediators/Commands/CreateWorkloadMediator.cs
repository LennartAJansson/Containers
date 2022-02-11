namespace WorkloadsProjector.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class CreateWorkloadMediator : IRequestHandler<CommandCreateWorkload, CommandWorkloadResponse>
    {
        private readonly ILogger<CreateWorkloadMediator> logger;

        public CreateWorkloadMediator(ILogger<CreateWorkloadMediator> logger) => this.logger = logger;

        public Task<CommandWorkloadResponse> Handle(CommandCreateWorkload request, CancellationToken cancellationToken)
        {
            logger.LogInformation(request.ToString());
            //TODO Update db
            return Task.FromResult(new CommandWorkloadResponse(1, $"{request}"));
        }
    }

}