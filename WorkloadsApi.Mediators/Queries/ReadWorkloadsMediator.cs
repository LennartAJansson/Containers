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

    public class ReadWorkloadsMediator : SqlQueryMediatorBase, IRequestHandler<QueryWorkloads, IEnumerable<QueryWorkloadResponse>>
    {
        private readonly ILogger<ReadWorkloadsMediator> logger;

        public ReadWorkloadsMediator(ILogger<ReadWorkloadsMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<IEnumerable<QueryWorkloadResponse>> Handle(QueryWorkloads request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (MySqlConnection connection = new MySqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                IEnumerable<Workload> workloads = await connection.QueryAsync<Workload, Assignment, Person, Workload>(
                    QueryStrings.SelectAllJoined,
                    (w, a, p) =>
                    {
                        w.Assignment = a;
                        w.Person = p;
                        a.Workloads.Add(w);
                        p.Workloads.Add(w);
                        return w;
                    }, splitOn: "AssignmentId, PersonId");

                return workloads.Select(w => new QueryWorkloadResponse(w.WorkloadId,
                        w.Start,
                        w.Stop,
                        w.Person!.ToUnJoinedQueryPersonResponse(),
                        w.Assignment!.ToUnJoinedQueryAssignmentResponse()));
            }
        }
    }
}