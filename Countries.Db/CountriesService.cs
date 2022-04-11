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
            .NoUpdate()
            .RunAsync();

        await context.SaveChangesAsync();

        foreach (Country country in countries)
        {
            await UpsertPrefixes(country.CountryId, country.Prefixes);
        }

        return countries;
    }

    public async Task<Country> UpsertCountryAsync(Country country)
    {
        await context.Countries.Upsert(country)
            .On(c => c.CountryId)
            .NoUpdate() //TODO Handle update of Country as well
            .RunAsync();

        await context.SaveChangesAsync();

        await UpsertPrefixes(country.CountryId, country.Prefixes);

        return country;
    }

    private async Task UpsertPrefixes(int countryId, IEnumerable<PhonePrefix> prefixes)
    {
        IQueryable<PhonePrefix>? actualPrefixes = context.PhonePrefixes.Where(p => p.CountryId == countryId);
        foreach (PhonePrefix? prefix in prefixes)
        {
            if (!actualPrefixes.Any(p => p.Prefix == prefix.Prefix))
            {
                context.PhonePrefixes.Add(prefix);
            }
        }
        foreach (PhonePrefix? prefix in actualPrefixes)
        {
            if (!prefixes.Any(p => p.Prefix == prefix.Prefix))
            {
                context.PhonePrefixes.Remove(prefix);
            }
        }

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
