// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkloadsApi.Controllers
{

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Workloads.Contract;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ILogger<CommandController> logger;
        private readonly IMediator mediator;

        public CommandController(ILogger<CommandController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonAsync([FromBody] CommandCreatePerson person) => Ok(await mediator.Send(person));

        [HttpPut("{personId}")]
        public async Task<IActionResult> UpdatePersonAsync(int personId, [FromBody] CommandUpdatePerson person) => Ok(await mediator.Send(person));

        [HttpDelete("{personId}")]
        public async Task<IActionResult> UpdatePersonAsync(int personId) => Ok(await mediator.Send(new CommandDeletePerson(personId)));
    }
}
