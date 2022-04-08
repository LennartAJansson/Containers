namespace CronJob.Client;

using CronJob.Model;

using Refit;

internal interface ICountryApiClient
{
    [Get("/v3.1/all")]
    Task<IEnumerable<CountryFromApi>> GetCountries();
}


