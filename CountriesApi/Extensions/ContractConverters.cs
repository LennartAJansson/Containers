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

        public static PhonePrefixResponse ToPhonePrefixResponse(this PhonePrefix phonePrefix)
        {
            return new PhonePrefixResponse(phonePrefix.Prefix);
        }
    }
}
