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

    public class ReadWorkloadMediator : SqlQueryMediatorBase, IRequestHandler<QueryWorkload, QueryWorkloadResponse>
    {
        private readonly ILogger<ReadWorkloadMediator> logger;

        public ReadWorkloadMediator(ILogger<ReadWorkloadMediator> logger, IOptions<ConnectionStrings> options)
            : base(options.Value) => this.logger = logger;

        public async Task<QueryWorkloadResponse> Handle(QueryWorkload request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (MySqlConnection connection = new MySqlConnection(connectionStrings.WorkloadsDb))
            {
                connection.Open();

                Workload workload = await connection.QuerySingleOrDefaultAsync<Workload>(
                                       @$"
SELECT `w`.*, `a`.*, `p`.*
FROM `Workloads` AS `w`
INNER JOIN `Assignments` AS `a` ON `w`.`AssignmentId` = `a`.`AssignmentId`
INNER JOIN `People` AS `p` ON `w`.`PersonId` = `p`.`PersonId`
WHERE `w`.`WorkloadId` = '{request.WorkloadId}'
ORDER BY `w`.`WorkloadId`"
);

                return new QueryWorkloadResponse(workload.WorkloadId,
                        workload.Start,
                        workload.Stop,
                        workload.Person!.ToUnJoinedQueryPersonResponse(),
                        workload.Assignment!.ToUnJoinedQueryAssignmentResponse());
            }
        }
    }
}