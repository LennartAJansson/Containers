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

    public class ReadPersonMediator : SqlQueryMediatorBase, IRequestHandler<QueryPerson, QueryPersonResponse>
    {
        private readonly ILogger<ReadPersonMediator> logger;

        public ReadPersonMediator(ILogger<ReadPersonMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<QueryPersonResponse> Handle(QueryPerson request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (MySqlConnection connection = new MySqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                Person person = await connection.QuerySingleOrDefaultAsync<Person>(
                    @$"
SELECT `p`.*, `w`.*
FROM `People` AS `p`
LEFT JOIN `Workloads` AS `w` ON `p`.`PersonId` = `w`.`PersonId`
WHERE `p`.`PersonId` = '{request.PersonId}'
ORDER BY `p`.`PersonId`"
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