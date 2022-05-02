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

        //[EnableCors]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CountryResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            CountriesRequest request = new();
            logger.LogDebug("Received {type}", request.GetType().Name);

            IEnumerable<CountryResponse> response = await mediator.Send(request);

            return Ok(response);
        }

        //[DisableCors]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{iso}")]
        public async Task<IActionResult> GetByIso(string iso)
        {
            CountryByIsoCountryRequest request = new(iso);
            logger.LogDebug("Requested {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }

        //[DisableCors]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode2(string code)
        {
            CountryByCountryCode2Request request = new(code);
            logger.LogDebug("Requested {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }

        //[DisableCors]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode3(string code)
        {
            CountryByCountryCode3Request request = new(code);
            logger.LogDebug("Requested {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }

        //[DisableCors]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PhonePrefixWithCountriesResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetPrefixes()
        {
            PhonePrefixDictionaryAllRequest request = new();
            logger.LogDebug("Requested {type}", request.GetType().Name);

            IEnumerable<PhonePrefixWithCountriesResponse> result = await mediator.Send(request);

            return Ok(result);
        }

        //[DisableCors]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhonePrefixWithCountriesResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetPrefixForPhoneNumber(string phoneNumber)
        {
            PhonePrefixDictionaryRequest request = new(phoneNumber);
            logger.LogDebug("Requested {type}", request.GetType().Name);

            PhonePrefixWithCountriesResponse result = await mediator.Send(request);

            return Ok(result);
        }

        //[DisableCors]
        //[HttpPost]
        //public Task<ActionResult> RefreshCountryInformation() => Task.FromResult(Ok() as ActionResult);
    }
}