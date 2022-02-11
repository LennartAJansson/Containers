namespace WorkloadsApi.Mediators.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class ReadAssignmentMediator : IRequestHandler<QueryAssignment, QueryAssignmentResponse>
    {
        private readonly ILogger<ReadAssignmentMediator> logger;

        public ReadAssignmentMediator(ILogger<ReadAssignmentMediator> logger) => this.logger = logger;

        public Task<QueryAssignmentResponse> Handle(QueryAssignment request, CancellationToken cancellationToken) =>
            throw new NotImplementedException();
    }
}