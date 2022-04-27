namespace CountriesApi.Controllers
{

    using CountriesApi.Contracts;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]/[action]")]
    public class CountriesController : ControllerBase
    {

        private readonly ILogger<CountriesController> logger;
        private readonly IMediator mediator;

        public CountriesController(ILogger<CountriesController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            CountriesRequest request = new CountriesRequest();
            logger.LogDebug("Received {type}", request.GetType().Name);

            IEnumerable<CountryResponse> response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{iso}")]
        public async Task<IActionResult> GetByIso(string iso)
        {
            CountryByIsoCountryRequest request = new CountryByIsoCountryRequest(iso);
            logger.LogDebug("Requested {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode2(string code)
        {
            CountryByCountryCode2Request request = new CountryByCountryCode2Request(code);
            logger.LogDebug("Requested {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode3(string code)
        {
            CountryByCountryCode3Request request = new CountryByCountryCode3Request(code);
            logger.LogDebug("Requested {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPrefixes()
        {
            PhonePrefixDictionaryAllRequest request = new PhonePrefixDictionaryAllRequest();
            logger.LogDebug("Requested {type}", request.GetType().Name);

            IEnumerable<PhonePrefixWithCountriesResponse> result = await mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetPrefixForPhoneNumber(string phoneNumber)
        {
            PhonePrefixDictionaryRequest request = new PhonePrefixDictionaryRequest(phoneNumber);
            logger.LogDebug("Requested {type}", request.GetType().Name);

            PhonePrefixWithCountriesResponse result = await mediator.Send(request);

            return Ok(result);
        }

        [HttpPost]
        public Task<ActionResult> RefreshCountryInformation()
        {
            return Task.FromResult(Ok() as ActionResult);
        }
    }
}