namespace Countries.Db;

using Countries.Model;

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

    public async Task<IEnumerable<Country>> CreateCountriesAsync(IEnumerable<Country> countries)
    {
        await context.Countries.AddRangeAsync(countries);
        await context.SaveChangesAsync();

        return countries;
    }

    public async Task<Country> CreateCountryAsync(Country country)
    {
        await context.Countries.AddAsync(country);
        await context.SaveChangesAsync();

        return country;
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
