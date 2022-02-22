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

    public class ReadAssignmentMediator : SqlQueryMediatorBase, IRequestHandler<QueryAssignment, QueryAssignmentResponse>
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
                string? sql = @$"
SELECT w.WorkloadId, w.Start, w.Stop, w.AssignmentId, w.PersonId, a.AssignmentId, a.CustomerName, a.Description, p.PersonId, p.Name
FROM Workloads AS w
JOIN Assignments AS a ON w.AssignmentId = a.AssignmentId
JOIN People AS p ON w.PersonId = p.PersonId
WHERE w.AssignmentId = '{request.AssignmentId}'";

                connection.Open();

                //Assignment? assignment = await connection.QuerySingleOrDefaultAsync<Assignment>(sql);
                IEnumerable<Assignment> assignments = await connection.QueryAsync<Workload, Assignment, Person, Assignment>(sql,
                    (w, a, p) =>
                {
                    w.Person = p;
                    w.Assignment = a;
                    a.Workloads.Add(w);
                    return a;
                }, splitOn: "AssignmentId, PersonId");

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