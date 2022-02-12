namespace WorkloadsApi.Mediators.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using NATS.Client;
    using NATS.Extensions.DependencyInjection;

    using Workloads.Contract;

    public class UpdatePersonMediator : NatsCommandMediatorBase, IRequestHandler<CommandUpdatePerson, CommandPersonResponse>
    {
        private readonly ILogger<UpdatePersonMediator> logger;

        public UpdatePersonMediator(ILogger<UpdatePersonMediator> logger, IConnection connection, IOptions<NatsProducer> options)
            : base(connection, options.Value)
            => this.logger = logger;

        public Task<CommandPersonResponse> Handle(CommandUpdatePerson request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            return Task.FromResult(new CommandPersonResponse(request.PersonId, $"{request}"));
        }
    }
}