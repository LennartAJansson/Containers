namespace CountriesApi.Contracts
{
    using Countries.Model;

    using MediatR;

    public record CountriesRequest() : IRequest<IEnumerable<CountryResponse>>;
    public record CountryByIsoCountryRequest(string IsoCountry) : IRequest<CountryResponse>;
    public record CountryByCountryCode2Request(string CountryCode2) : IRequest<CountryResponse>;
    public record CountryByCountryCode3Request(string CountryCode3) : IRequest<CountryResponse>;
    public record CountryResponse(string IsoCountry, string CountryCode2, string CountryCode3, string CountryName, IEnumerable<PrefixResponse> PhonePrefixes);
    public record PrefixResponse(string Prefix);

    public record PrefixesRequest() : IRequest<IEnumerable<PhonePrefix>>;
    public record PhonePrefixDictionaryRequest(string phoneNumber) : IRequest<PhonePrefixWithCountriesResponse>;
    public record PhonePrefixDictionaryAllRequest() : IRequest<IEnumerable<PhonePrefixWithCountriesResponse>>;
    public record PhonePrefixWithCountriesResponse(string Prefix, IEnumerable<CountryWithoutPrefixResponse> Countries);
    public record CountryWithoutPrefixResponse(string IsoCountry, string CountryCode2, string CountryCode3, string CountryName);
}
