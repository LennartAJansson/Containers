namespace CountriesApi.Mediators
{

    using CountriesApi.Contracts;
    using CountriesApi.Extensions;

    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    public class PhonePrefixDictionaryHandler :
        IRequestHandler<PhonePrefixDictionaryRequest, PhonePrefixWithCountriesResponse>,
        IRequestHandler<PhonePrefixDictionaryAllRequest, IEnumerable<PhonePrefixWithCountriesResponse>>
    {
        private readonly PhonePrefixDictionary phonePrefixes;

        public PhonePrefixDictionaryHandler(ILogger<PhonePrefixDictionaryHandler> logger, PhonePrefixDictionary phonePrefixes)
        {
            this.phonePrefixes = phonePrefixes;
        }

        public Task<PhonePrefixWithCountriesResponse> Handle(PhonePrefixDictionaryRequest request, CancellationToken cancellationToken)
        {
            PhonePrefixWithCountriesResponse result = phonePrefixes.GetPrefix(Convert.ToInt64(request.phoneNumber)).ToPrefixWithCountriesResponse();
            return Task.FromResult(result);
        }


        public Task<IEnumerable<PhonePrefixWithCountriesResponse>> Handle(PhonePrefixDictionaryAllRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<PhonePrefixWithCountriesResponse> result = phonePrefixes.Select(p => p.Value.ToPrefixWithCountriesResponse());
            return Task.FromResult(result);
        }
    }
}
