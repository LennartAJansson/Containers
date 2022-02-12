namespace WorkloadsApi.Mediators.Queries
{
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Dapper;

    using MediatR;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Workloads.Contract;
    using Workloads.Model;

    public class ReadPeopleMediator : SqlQueryMediatorBase, IRequestHandler<QueryPeople, IEnumerable<QueryPersonResponse>>
    {
        private readonly ILogger<ReadPeopleMediator> logger;

        public ReadPeopleMediator(ILogger<ReadPeopleMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<IEnumerable<QueryPersonResponse>> Handle(QueryPeople request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (SqlConnection connection = new SqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                IEnumerable<Person> people = await connection.QueryAsync<Person>(
                    @$"SELECT * FROM People ORDER BY PersonId"
                );

                return people.Select(p =>
                    new QueryPersonResponse(p.PersonId,
                        p.Name!,
                        p.Workloads.Select(w =>
                        new QueryWorkloadResponse(w.WorkloadId,
                            w.Start,
                            w.Stop,
                            w.Person!.ToUnJoinedQueryPersonResponse(),
                            w.Assignment!.ToUnJoinedQueryAssignmentResponse()))));
            }
        }
    }
}