namespace CountriesApi.Mediators
{
    using Countries.Model;

    using CountriesApi.Contracts;

    using MediatR;

    using Microsoft.Extensions.Options;

    using MySql.Data.MySqlClient;

    using System.Threading;
    using System.Threading.Tasks;

    public class CountryQueryMediators : SqlQueryMediatorBase, IRequestHandler<CountriesRequest, IEnumerable<CountryResponse>>,
        IRequestHandler<CountryByIsoRequest, CountryResponse>,
        IRequestHandler<CountryByCode2Request, CountryResponse>,
        IRequestHandler<CountryByCode3Request, CountryResponse>
    {
        private readonly ILogger<CountryQueryMediators> logger;

        public CountryQueryMediators(ILogger<CountryQueryMediators> logger, IOptions<ConnectionStrings> options)
        : base(options.Value)
        {
            this.logger = logger;
        }

        public Task<IEnumerable<CountryResponse>> Handle(CountriesRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CountryResponse> Handle(CountryByIsoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CountryResponse> Handle(CountryByCode2Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CountryResponse> Handle(CountryByCode3Request request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using (MySqlConnection connection = new MySqlConnection(connectionStrings.CountriesDb))
            {
                connection.Open();

                //IEnumerable<Assignment> assignments = await connection.QueryAsync<Workload, Assignment, Person, Assignment>(
                //    QueryStrings.SelectAllJoinedWhereAssignment,
                //    (w, a, p) =>
                //    {
                //        w.Assignment = a;
                //        w.Person = p;
                //        a.Workloads.Add(w);
                //        p.Workloads.Add(w);
                //        return a;
                //    }, new { p1 = request.AssignmentId }, splitOn: "AssignmentId, PersonId");

                //Assignment? assignment = assignments.FirstOrDefault();

                //if (assignment == null)
                //{
                //    return null;
                //}
                //else
                //{
                //    return new QueryAssignmentResponse(assignment.AssignmentId,
                //        assignment.CustomerName!,
                //        assignment.Description!,
                //        assignment.Workloads.Select(w =>
                //        new QueryWorkloadResponse(w.WorkloadId,
                //            w.Start,
                //            w.Stop,
                //            w.Person!.ToUnJoinedQueryPersonResponse(),
                //            w.Assignment!.ToUnJoinedQueryAssignmentResponse())));
                //}

                return Task.FromResult(new CountryResponse());
            }
        }
    }
}
