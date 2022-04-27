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
            logger.LogDebug("Received {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetByCode2([FromBody] CountryByCountryCode2Request request)
        {
            logger.LogDebug("Received {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> GetByCode3([FromBody] CountryByCountryCode3Request request)
        {
            logger.LogDebug("Received {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPrefixes()
        {
            IEnumerable<PhonePrefixWithCountriesResponse> result = await mediator.Send(new PhonePrefixDictionaryAllRequest());
            return Ok(result);
        }

        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetPrefixForPhoneNumber(string phoneNumber)
        {
            PhonePrefixWithCountriesResponse result = await mediator.Send(new PhonePrefixDictionaryRequest(phoneNumber));
            return Ok(result);
        }

        [HttpPost]
        public Task<ActionResult> RefreshCountryInformation()
        {
            return Task.FromResult(Ok() as ActionResult);
        }
    }
}