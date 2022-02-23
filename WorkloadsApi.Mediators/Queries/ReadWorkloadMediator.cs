namespace WorkloadsApi.Mediators.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    using Dapper;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using MySql.Data.MySqlClient;

    using Workloads.Contract;
    using Workloads.Model;

    public class ReadWorkloadMediator : SqlQueryMediatorBase, IRequestHandler<QueryWorkload, QueryWorkloadResponse?>
    {
        private readonly ILogger<ReadWorkloadMediator> logger;

        public ReadWorkloadMediator(ILogger<ReadWorkloadMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<QueryWorkloadResponse?> Handle(QueryWorkload request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (MySqlConnection connection = new MySqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                IEnumerable<Workload> workloads = await connection.QueryAsync<Workload, Assignment, Person, Workload>(
                    QueryStrings.SelectAllJoinedWhereWorkload,
                    (w, a, p) =>
                    {
                        w.Assignment = a;
                        w.Person = p;
                        a.Workloads.Add(w);
                        p.Workloads.Add(w);
                        return w;
                    }, new { p1 = request.WorkloadId }, splitOn: "AssignmentId, PersonId");

                Workload? workload = workloads.FirstOrDefault();

                if (workload == null)
                {
                    return null;
                }
                else
                {
                    return new QueryWorkloadResponse(workload.WorkloadId,
                        workload.Start,
                        workload.Stop,
                        workload.Person!.ToUnJoinedQueryPersonResponse(),
                        workload.Assignment!.ToUnJoinedQueryAssignmentResponse());
                }
            }
        }
    }
}