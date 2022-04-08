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
            CountriesRequest? request = new CountriesRequest();
            logger.LogDebug("Received {type}", request.GetType().Name);

            IEnumerable<CountryResponse> response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIso()
        {
            CountryByIsoRequest? request = new CountryByIsoRequest();
            logger.LogDebug("Received {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetByCode2()
        {
            CountryByCode2Request? request = new CountryByCode2Request();
            logger.LogDebug("Received {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetByCode3()
        {
            CountryByCode3Request? request = new CountryByCode3Request();
            logger.LogDebug("Received {type}", request.GetType().Name);

            CountryResponse response = await mediator.Send(request);

            return Ok(response);
        }
    }
}