namespace WorkloadsApi.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Prometheus;

using Workloads.Contract;

[Route("api/[controller]/[action]")]
[ApiController]
public class CommandController : MetricsControllerBase
{
    private readonly ILogger<CommandController> logger;

    public CommandController(ILogger<CommandController> logger, IMediator mediator)
        : base(mediator)
        => this.logger = logger;

    //People
    [HttpPost]
    public async Task<IActionResult> CreatePersonAsync([FromBody] CommandCreatePerson person)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse? result = await mediator.Send(person);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePersonAsync([FromBody] CommandUpdatePerson person)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse? result = await mediator.Send(person);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }

    [HttpDelete("{personId}")]
    public async Task<IActionResult> DeletePersonAsync(Guid personId)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse? result = await mediator.Send(new CommandDeletePerson(personId));
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }

    //Assignments
    [HttpPost]
    public async Task<IActionResult> CreateAssignmentAsync([FromBody] CommandCreateAssignment assignment)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse? result = await mediator.Send(assignment);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAssignmentAsync([FromBody] CommandUpdateAssignment assignment)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse? result = await mediator.Send(assignment);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }

    [HttpDelete("{assignmentId}")]
    public async Task<IActionResult> DeleteAssignmentAsync(Guid assignmentId)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse? result = await mediator.Send(new CommandDeleteAssignment(assignmentId));
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }

    //Workloads
    [HttpPost]
    public async Task<IActionResult> CreateWorkloadAsync([FromBody] CommandCreateWorkload workload)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse? result = await mediator.Send(workload);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWorkloadAsync([FromBody] CommandUpdateWorkload workload)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse? result = await mediator.Send(workload);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }

    [HttpDelete("{workloadId}")]
    public async Task<IActionResult> DeleteWorkloadAsync(Guid workloadId)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse? result = await mediator.Send(new CommandDeleteWorkload(workloadId));
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime./*Labels(DateTime.Now.ToString("hh:mm:ss:ffff")).*/Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Inc();

        return Ok(result);
    }
}
