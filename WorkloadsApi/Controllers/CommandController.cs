// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkloadsApi.Controllers
{

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Prometheus;

    using Workloads.Contract;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        public static Gauge requestExecuteTime = Metrics.CreateGauge("createperson_executiontime", "Counts total execution time for sending requests",
            new GaugeConfiguration
            {
                LabelNames = new[] { "time" }
            });

        public static Counter counter = Metrics.CreateCounter("createperson_counter", "",
            new CounterConfiguration
            {
                LabelNames = new[] { "requests" }
            });

        private readonly ILogger<CommandController> logger;
        private readonly IMediator mediator;

        public CommandController(ILogger<CommandController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonAsync([FromBody] CommandCreatePerson person)
        {
            DateTime startDateTime = DateTime.Now;
            CommandPersonResponse? result = await mediator.Send(person);
            DateTime endDateTime = DateTime.Now;

            requestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
            counter.Inc();

            return Ok(result);
        }

        [HttpPut("{personId}")]
        public async Task<IActionResult> UpdatePersonAsync(int personId, [FromBody] CommandUpdatePerson person) => Ok(await mediator.Send(person));

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePersonAsync(int personId) => Ok(await mediator.Send(new CommandDeletePerson(personId)));

        [HttpPost]
        public async Task<IActionResult> CreateAssignmentAsync([FromBody] CommandCreateAssignment assignment)
        {
            DateTime startDateTime = DateTime.Now;
            CommandAssignmentResponse? result = await mediator.Send(assignment);
            DateTime endDateTime = DateTime.Now;

            requestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
            counter.Inc();

            return Ok(result);
        }

        [HttpPut("{assignmentId}")]
        public async Task<IActionResult> UpdateAssignmentAsync(int assignmentId, [FromBody] CommandUpdateAssignment assignment) => Ok(await mediator.Send(assignment));

        [HttpDelete("{assignmentId}")]
        public async Task<IActionResult> DeleteAssignmentAsync(int assignmentId) => Ok(await mediator.Send(new CommandDeleteAssignment(assignmentId)));

    }
}
