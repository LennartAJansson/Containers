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

    public class ReadPersonMediator : SqlQueryMediatorBase, IRequestHandler<QueryPerson, QueryPersonResponse>
    {
        private readonly ILogger<ReadPersonMediator> logger;

        public ReadPersonMediator(ILogger<ReadPersonMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<QueryPersonResponse> Handle(QueryPerson request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (SqlConnection connection = new SqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                Person person = await connection.QuerySingleOrDefaultAsync<Person>(
                    @$"SELECT * FROM People WHERE PersonId = {request.PersonId}"
                );

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