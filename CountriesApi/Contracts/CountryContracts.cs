namespace CountriesApi.Contracts
{
    using MediatR;

    public record CountriesRequest() : IRequest<IEnumerable<CountryResponse>>;
    public record CountryByIsoCountryRequest(string IsoCountry) : IRequest<CountryResponse>;
    public record CountryByCountryCode2Request(string CountryCode2) : IRequest<CountryResponse>;
    public record CountryByCountryCode3Request(string CountryCode3) : IRequest<CountryResponse>;
    public record CountryResponse(string IsoCountry, string CountryCode2, string CountryCode3, string CountryName, IEnumerable<PhonePrefixResponse> PhonePrefixes);
    public record PhonePrefixResponse(string Prefix);
}
