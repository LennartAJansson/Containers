namespace WorkloadsApi.Mediators.Commands;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NATS.Client;
using NATS.Extensions.DependencyInjection;

using Workloads.Contract;

public class DeleteAssignmentMediator : NatsCommandMediatorBase, IRequestHandler<CommandDeleteAssignment, CommandAssignmentResponse>
{
    private readonly ILogger<DeleteAssignmentMediator> logger;

    public DeleteAssignmentMediator(ILogger<DeleteAssignmentMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        : base(connection, options.Value)
        => this.logger = logger;

    public Task<CommandAssignmentResponse> Handle(CommandDeleteAssignment request, CancellationToken cancellationToken)
    {
        logger.LogDebug("");
        return Task.FromResult(new CommandAssignmentResponse(request.AssignmentId, $"{request}"));
    }
}
