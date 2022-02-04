// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkloadsApi.Controllers
{
    using MediatR;

    using Microsoft.AspNetCore.Mvc;

    using Workloads.Contract;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IMediator mediator;

        public QueryController(IMediator mediator) => this.mediator = mediator;

        [HttpGet]
        //[Route("people")]
        public async Task<IActionResult> GetPeopleAsync() => Ok(await mediator.Send(new QueryPeople()));

        [HttpGet("{personId}")]
        //[Route("people")]
        public async Task<IActionResult> GetPersonAsync(int personId) => Ok(await mediator.Send(new QueryPerson(personId)));
    }
}
