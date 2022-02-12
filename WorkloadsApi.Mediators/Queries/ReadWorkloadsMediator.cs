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

    public class ReadWorkloadsMediator : SqlQueryMediatorBase, IRequestHandler<QueryWorkloads, IEnumerable<QueryWorkloadResponse>>
    {
        private readonly ILogger<ReadWorkloadsMediator> logger;

        public ReadWorkloadsMediator(ILogger<ReadWorkloadsMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<IEnumerable<QueryWorkloadResponse>> Handle(QueryWorkloads request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (SqlConnection connection = new SqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                IEnumerable<Workload> workloads = await connection.QueryAsync<Workload>(
                    @$"SELECT * FROM Workloads ORDER BY WorkloadId"
                );

                return workloads.Select(w => new QueryWorkloadResponse(w.WorkloadId,
                        w.Start,
                        w.Stop,
                        w.Person!.ToUnJoinedQueryPersonResponse(),
                        w.Assignment!.ToUnJoinedQueryAssignmentResponse()));
            }
        }
    }
}