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

    public class ReadWorkloadMediator : SqlQueryMediatorBase, IRequestHandler<QueryWorkload, QueryWorkloadResponse>
    {
        private readonly ILogger<ReadWorkloadMediator> logger;

        public ReadWorkloadMediator(ILogger<ReadWorkloadMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<QueryWorkloadResponse> Handle(QueryWorkload request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (SqlConnection connection = new SqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                Workload workload = await connection.QuerySingleOrDefaultAsync<Workload>(
                    @$"SELECT * FROM Workloads WHERE WorkloadId = {request.WorkloadId}"
                );

                return new QueryWorkloadResponse(workload.WorkloadId,
                        workload.Start,
                        workload.Stop,
                        workload.Person!.ToUnJoinedQueryPersonResponse(),
                        workload.Assignment!.ToUnJoinedQueryAssignmentResponse());
            }
        }
    }
}