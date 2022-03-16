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

    public class ReadAssignmentsMediator : SqlQueryMediatorBase, IRequestHandler<QueryAssignments, IEnumerable<QueryAssignmentResponse>>
    {
        private readonly ILogger<ReadAssignmentsMediator> logger;

        public ReadAssignmentsMediator(ILogger<ReadAssignmentsMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<IEnumerable<QueryAssignmentResponse>> Handle(QueryAssignments request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (MySqlConnection connection = new MySqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                IEnumerable<Assignment> assignments = await connection.QueryAsync<Workload, Assignment, Person, Assignment>(
                    QueryStrings.SelectAllJoined,
                    (w, a, p) =>
                    {
                        w.Assignment = a;
                        w.Person = p;
                        a.Workloads.Add(w);
                        p.Workloads.Add(w);
                        return a;
                    }, splitOn: "AssignmentId, PersonId");
                //Lennart Fixa detta

                return assignments.Select(a =>
                    new QueryAssignmentResponse(a.AssignmentId,
                        a.CustomerName!,
                        a.Description!,
                        a.Workloads.Select(w =>
                        new QueryWorkloadResponse(w.WorkloadId,
                            w.Start,
                            w.Stop,
                            w.Person!.ToUnJoinedQueryPersonResponse(),
                            w.Assignment!.ToUnJoinedQueryAssignmentResponse()))));
            }
        }
    }
}

