namespace CountriesApi.Mediators
{
    using Countries.Model;

    using CountriesApi.Contracts;
    using CountriesApi.Extensions;

    using Dapper;

    using MediatR;

    using Microsoft.Extensions.Options;

    using MySql.Data.MySqlClient;

    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CountrySqlQueryMediators : SqlQueryMediatorBase, IRequestHandler<CountriesRequest, IEnumerable<CountryResponse>>,
        IRequestHandler<CountryByIsoCountryRequest, CountryResponse>,
        IRequestHandler<CountryByCountryCode2Request, CountryResponse>,
        IRequestHandler<CountryByCountryCode3Request, CountryResponse>
    {
        private readonly ILogger<CountrySqlQueryMediators> logger;

        public CountrySqlQueryMediators(ILogger<CountrySqlQueryMediators> logger, IOptions<ConnectionStrings> options)
        : base(options.Value)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<CountryResponse>> Handle(CountriesRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using MySqlConnection connection = new MySqlConnection(connectionStrings.CountriesDb);
            connection.Open();

            IEnumerable<Country> countries = await connection.QueryAsync<PhonePrefix, Country, Country>(
                QueryStrings.SelectAllCountries,
                (p, c) =>
                {
                    p.CountryId = c.CountryId;
                    p.Country = c;
                    c.Prefixes.Add(p);
                    return c;
                }, splitOn: "CountryId");

            return countries.Select(c => c.ToCountryResponse());
        }

        public async Task<CountryResponse> Handle(CountryByIsoCountryRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using MySqlConnection connection = new MySqlConnection(connectionStrings.CountriesDb);
            connection.Open();

            IEnumerable<Country> countries = await connection.QueryAsync<PhonePrefix, Country, Country>(
                QueryStrings.SelectCountryByIso,
                (p, c) =>
                {
                    p.CountryId = c.CountryId;
                    p.Country = c;
                    c.Prefixes.Add(p);
                    return c;
                }, new { p1 = request.IsoCountry }, splitOn: "CountryId");

            return countries.FirstOrDefault().ToCountryResponse();
        }

        public async Task<CountryResponse> Handle(CountryByCountryCode2Request request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using MySqlConnection connection = new MySqlConnection(connectionStrings.CountriesDb);
            connection.Open();

            IEnumerable<Country> countries = await connection.QueryAsync<PhonePrefix, Country, Country>(
                QueryStrings.SelectCountryByCode2,
                (p, c) =>
                {
                    p.CountryId = c.CountryId;
                    p.Country = c;
                    c.Prefixes.Add(p);
                    return c;
                }, new { p1 = request.CountryCode2 }, splitOn: "CountryId");

            return countries.FirstOrDefault().ToCountryResponse();
        }

        public async Task<CountryResponse> Handle(CountryByCountryCode3Request request, CancellationToken cancellationToken)
        {
            logger.LogDebug("");
            using MySqlConnection connection = new MySqlConnection(connectionStrings.CountriesDb);
            connection.Open();

            IEnumerable<Country> countries = await connection.QueryAsync<PhonePrefix, Country, Country>(
                QueryStrings.SelectCountryByCode3,
                (p, c) =>
                {
                    p.CountryId = c.CountryId;
                    p.Country = c;
                    c.Prefixes.Add(p);
                    return c;
                }, new { p1 = request.CountryCode3 }, splitOn: "CountryId");

            return countries.FirstOrDefault().ToCountryResponse();
        }
    }
}
