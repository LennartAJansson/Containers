namespace WorkloadsApi.Mediators.Queries
{
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    using Dapper;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Workloads.Contract;
    using Workloads.Model;

    public class ReadAssignmentMediator : SqlQueryMediatorBase, IRequestHandler<QueryAssignment, QueryAssignmentResponse>
    {
        private readonly ILogger<ReadAssignmentMediator> logger;

        public ReadAssignmentMediator(ILogger<ReadAssignmentMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value)
            => this.logger = logger;

        public async Task<QueryAssignmentResponse> Handle(QueryAssignment request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (SqlConnection connection = new SqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                Assignment assignment = await connection.QuerySingleOrDefaultAsync<Assignment>(
                    @$"SELECT * FROM Assignment WHERE AssignmentId = {request.AssignmentId}"
                );

                return new QueryAssignmentResponse(assignment.AssignmentId,
                        assignment.CustomerName!,
                        assignment.Description!,
                        assignment.Workloads.Select(w =>
                        new QueryWorkloadResponse(w.WorkloadId,
                            w.Start,
                            w.Stop,
                            w.Person!.ToUnJoinedQueryPersonResponse(),
                            w.Assignment!.ToUnJoinedQueryAssignmentResponse())));
            }

        }
    }
}