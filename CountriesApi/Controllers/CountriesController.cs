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
        private readonly CountryDictionary dictionary;

        public CountriesController(ILogger<CountriesController> logger, IMediator mediator, CountryDictionary dictionary)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.dictionary = dictionary;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            CountriesRequest? request = new CountriesRequest();
            logger.LogDebug("Received {type}", request.GetType().Name);

            IEnumerable<CountryResponse> response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetByIso([FromBody] CountryByIsoCountryRequest request)
        {
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

            return Ok(dictionary);
        }

    }
}