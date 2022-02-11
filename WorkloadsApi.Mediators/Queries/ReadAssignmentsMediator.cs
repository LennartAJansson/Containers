namespace WorkloadsApi.Mediators.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    public class ReadAssignmentsMediator : IRequestHandler<QueryAssignments, IEnumerable<QueryAssignmentResponse>>
    {
        private readonly ILogger<ReadAssignmentsMediator> logger;

        public ReadAssignmentsMediator(ILogger<ReadAssignmentsMediator> logger) => this.logger = logger;

        public Task<IEnumerable<QueryAssignmentResponse>> Handle(QueryAssignments request, CancellationToken cancellationToken) =>
            throw new NotImplementedException();
    }
}