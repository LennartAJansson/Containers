namespace WorkloadsApi.Mediators.Queries
{
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    using Dapper;

    using MediatR;

    using Microsoft.Extensions.Logging;

    using Workloads.Contract;
    using Workloads.Model;

    public class ReadPeopleMediator : IRequestHandler<QueryPeople, IEnumerable<QueryPersonResponse>>
    {
        private readonly ILogger<ReadPeopleMediator> logger;

        public ReadPeopleMediator(ILogger<ReadPeopleMediator> logger) => this.logger = logger;

        public async Task<IEnumerable<QueryPersonResponse>> Handle(QueryPeople request, CancellationToken cancellationToken)
        {
            using (SqlConnection connection = new SqlConnection(""))
            {
                connection.Open();

                IEnumerable<Person> people = await connection.QueryAsync<Person>(
                    @$"SELECT * FROM People ORDER BY PersonId"
                );

                return people.Select(p => new QueryPersonResponse(p.PersonId, p.Name, p.Workloads.Select(w => new QueryWorkloadResponse())));
            }
        }
    }
}