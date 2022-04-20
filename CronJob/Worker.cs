namespace CronJob
{
    using Common;

    using Countries.Db;
    using Countries.Model;

    using CronJob.Client;
    using CronJob.Model;

    internal class Worker
    {
        private readonly ILogger<Worker> logger;
        private readonly ApplicationInfo info;
        private readonly ICountryApiClient client;
        private readonly ICountriesService service;

        public Worker(ILogger<Worker> logger, ApplicationInfo info, ICountryApiClient client, ICountriesService service)
        {
            this.logger = logger;
            this.info = info;
            this.client = client;
            this.service = service;
        }

        public async Task ExecuteAsync()
        {
            logger.LogInformation($"{info.SemanticVersion} ({info.Description})");
            IEnumerable<Country> countries = await TranslateCountries(await GetCountriesFromApi());
            await service.UpsertCountriesAsync(countries);
        }

        private async Task<IEnumerable<CountryFromApi>> GetCountriesFromApi()
        {
            IEnumerable<CountryFromApi> result = await client.GetCountries();

            return result;
        }

        private Task<IEnumerable<Country>> TranslateCountries(IEnumerable<CountryFromApi> countriesFromApi)
        {
            IEnumerable<Country> countries = countriesFromApi.Where(c =>
                !string.IsNullOrWhiteSpace(c.IsoCountry) &&
                !string.IsNullOrWhiteSpace(c.PhonePrefix.Prefix) &&
                c.PhonePrefix.Suffixes != null)
                .Select(a =>
                {
                    int cId = Convert.ToInt32(a.IsoCountry);

                    return new Country
                    {
                        CountryId = Convert.ToInt32(a.IsoCountry),
                        CountryName = a.Name.Common,
                        CountryCode2 = a.CountryCode2,
                        CountryCode3 = a.CountryCode3,
                        IsoCountry = a.IsoCountry,
                        Prefixes = a.PhonePrefix.Suffixes.Select(p => new PhonePrefix { CountryId = cId, Prefix = $"{a.PhonePrefix.Prefix}{p}" }).ToList()
                    };
                });

            return Task.FromResult(countries);
        }
    }
}
