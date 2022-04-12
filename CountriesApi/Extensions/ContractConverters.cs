namespace CountriesApi.Extensions
{
    using Countries.Model;

    using CountriesApi.Contracts;

    public static class ContractConverters
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse(country.IsoCountry,
                        country.CountryCode2,
                        country.CountryCode3,
                        country.CountryName,
                        country.Prefixes.Select(p => p.ToPhonePrefixResponse()));
        }

        public static PrefixResponse ToPhonePrefixResponse(this PhonePrefix phonePrefix)
        {
            return new PrefixResponse(phonePrefix.Prefix);
        }

        public static PhonePrefixWithCountriesResponse ToPrefixWithCountriesResponse(this PhonePrefixWithCountries prefix)
        {
            return new PhonePrefixWithCountriesResponse(prefix.Prefix, prefix.Countries.Select(c => c.ToCountryWithoutPrefixResponse()));
        }

        public static CountryWithoutPrefixResponse ToCountryWithoutPrefixResponse(this Country country)
        {
            return new CountryWithoutPrefixResponse(country.IsoCountry, country.CountryCode2, country.CountryCode3, country.CountryName);
        }

    }
}
