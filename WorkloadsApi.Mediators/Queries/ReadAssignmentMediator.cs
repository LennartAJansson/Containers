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

    public class ReadAssignmentMediator : SqlQueryMediatorBase, IRequestHandler<QueryAssignment, QueryAssignmentResponse?>
    {
        private readonly ILogger<ReadAssignmentMediator> logger;

        public ReadAssignmentMediator(ILogger<ReadAssignmentMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value)
            => this.logger = logger;

        public async Task<QueryAssignmentResponse?> Handle(QueryAssignment request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (MySqlConnection connection = new MySqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                IEnumerable<Assignment> assignments = await connection.QueryAsync<Workload, Assignment, Person, Assignment>(
                    QueryStrings.SelectAllJoinedWhereAssignment,
                    (w, a, p) =>
                    {
                        w.Assignment = a;
                        w.Person = p;
                        a.Workloads.Add(w);
                        p.Workloads.Add(w);
                        return a;
                    }, new { p1 = request.AssignmentId }, splitOn: "AssignmentId, PersonId");

                Assignment? assignment = assignments.FirstOrDefault();

                if (assignment == null)
                {
                    return null;
                }
                else
                {
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
}