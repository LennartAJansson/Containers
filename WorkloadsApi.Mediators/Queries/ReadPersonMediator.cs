namespace WorkloadsApi.Mediators.Queries
{
    using Dapper;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using MySql.Data.MySqlClient;

    using System.Threading;
    using System.Threading.Tasks;

    using Workloads.Contract;
    using Workloads.Model;

    public class ReadPersonMediator : SqlQueryMediatorBase, IRequestHandler<QueryPerson, QueryPersonResponse>
    {
        private readonly ILogger<ReadPersonMediator> logger;

        public ReadPersonMediator(ILogger<ReadPersonMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value)
        {
            this.logger = logger;
        }

        public async Task<QueryPersonResponse> Handle(QueryPerson request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (MySqlConnection connection = new MySqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                IEnumerable<Person> people = await connection.QueryAsync<Workload, Assignment, Person, Person>(
                    QueryStrings.SelectAllJoinedWherePerson,
                    (w, a, p) =>
                    {
                        w.Assignment = a;
                        w.Person = p;
                        a.Workloads.Add(w);
                        p.Workloads.Add(w);
                        return p;
                    }, new { p1 = request.PersonId }, splitOn: "AssignmentId, PersonId");

                Person person = people.FirstOrDefault();

                if (person == null)
                {
                    return null;
                }
                else
                {
                    return new QueryPersonResponse(person.PersonId,
                        person.Name!,
                        person.Workloads.Select(w =>
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