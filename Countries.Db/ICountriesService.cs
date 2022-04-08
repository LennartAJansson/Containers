namespace Countries.Db;

using Countries.Model;

public interface ICountriesService
{
    Task<Country> CreateCountryAsync(Country country);
    Task<IEnumerable<Country>> CreateCountriesAsync(IEnumerable<Country> countries);

    Task<Country?> GetCountryByIdAsync(int countryId);
    Task<Country?> GetCountryByCodeAsync(string code);
    Task<IEnumerable<Country>> GetCountriesAsync();
}
