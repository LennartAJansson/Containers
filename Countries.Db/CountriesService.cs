namespace Countries.Db;

using Countries.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

public class CountriesService : ICountriesService
{
    private readonly ILogger<CountriesService> logger;
    private readonly ICountriesDbContext context;

    public CountriesService(ILogger<CountriesService> logger, ICountriesDbContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    public async Task<IEnumerable<Country>> UpsertCountriesAsync(IEnumerable<Country> countries)
    {
        await context.Countries.UpsertRange(countries)
            .On(c => c.CountryId)
            //.WhenMatched(c => c)
            .RunAsync();

        await context.SaveChangesAsync();

        foreach (Country country in countries)
        {
            await UpsertPrefixes(country.Prefixes);
        }

        return countries;
    }

    public async Task<Country> UpsertCountryAsync(Country country)
    {
        await context.Countries.Upsert(country)
            .On(c => c.CountryId)
            //.WhenMatched(c => c)
            .RunAsync();

        await context.SaveChangesAsync();

        await UpsertPrefixes(country.Prefixes);

        return country;
    }

    private async Task UpsertPrefixes(IEnumerable<PhonePrefix> prefixes)
    {
        await context.PhonePrefixes.UpsertRange(prefixes)
            .On(p => p.Prefix)
            .WhenMatched(p => new PhonePrefix { CountryId = p.CountryId, Prefix = p.Prefix })
            .RunAsync();

        await context.SaveChangesAsync();
    }

    public Task<IEnumerable<Country>> GetCountriesAsync()
    {
        return Task.FromResult(context.Countries.AsEnumerable());
    }

    public Task<Country?> GetCountryByCodeAsync(string code)
    {
        return Task.FromResult(context.Countries.FirstOrDefault(c => c.CountryCode2 == code));
    }

    public Task<Country?> GetCountryByIdAsync(int countryId)
    {
        return Task.FromResult(context.Countries.FirstOrDefault(c => c.CountryId == countryId));
    }
}
