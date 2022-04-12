namespace CountriesApi.Mediators
{
    using Countries.Model;

    using CountriesApi.Contracts;

    using Dapper;

    using MediatR;

    using Microsoft.Extensions.Options;

    using MySql.Data.MySqlClient;

    using System.Threading;
    using System.Threading.Tasks;

    public class PrefixSqlQueryMediators : SqlQueryMediatorBase, IRequestHandler<PrefixesRequest, IEnumerable<PhonePrefix>>
    {
        private readonly ILogger<PrefixSqlQueryMediators> logger;

        public PrefixSqlQueryMediators(ILogger<PrefixSqlQueryMediators> logger, IOptions<ConnectionStrings> options)
            : base(options.Value)
        {
            this.logger = logger;
        }
        public async Task<IEnumerable<PhonePrefix>> Handle(PrefixesRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using MySqlConnection connection = new MySqlConnection(connectionStrings.CountriesDb);
            connection.Open();

            IEnumerable<PhonePrefix> prefixes = await connection.QueryAsync<PhonePrefix, Country, PhonePrefix>(
                QueryStrings.SelectAllPrefixes,
                (p, c) =>
                {
                    p.CountryId = c.CountryId;
                    p.Country = c;
                    c.Prefixes.Add(p);
                    return p;
                }, splitOn: "CountryId");

            return prefixes;
        }
    }
}
