namespace CountriesApi.Contracts
{
    using MediatR;

    public record CountriesRequest() : IRequest<IEnumerable<CountryResponse>>;
    public record CountryByIsoRequest() : IRequest<CountryResponse>;
    public record CountryByCode2Request() : IRequest<CountryResponse>;
    public record CountryByCode3Request() : IRequest<CountryResponse>;
    public record CountryResponse();
    public record PhonePrefixResponse();
}
