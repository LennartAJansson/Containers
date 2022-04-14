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
    {
        this.logger = logger;
    }

    //People
    [HttpPost]
    public async Task<IActionResult> CreatePersonAsync([FromBody] CommandCreatePerson request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);
        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePersonAsync([FromBody] CommandUpdatePerson request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpDelete("{personId}")]
    public async Task<IActionResult> DeletePersonAsync(Guid personId)
    {
        CommandDeletePerson request = new CommandDeletePerson(personId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    //Assignments
    [HttpPost]
    public async Task<IActionResult> CreateAssignmentAsync([FromBody] CommandCreateAssignment request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAssignmentAsync([FromBody] CommandUpdateAssignment request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpDelete("{assignmentId}")]
    public async Task<IActionResult> DeleteAssignmentAsync(Guid assignmentId)
    {
        CommandDeleteAssignment request = new CommandDeleteAssignment(assignmentId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    //Workloads
    [HttpPost]
    public async Task<IActionResult> CreateWorkloadAsync([FromBody] CommandCreateWorkload request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWorkloadAsync([FromBody] CommandUpdateWorkload request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpDelete("{workloadId}")]
    public async Task<IActionResult> DeleteWorkloadAsync(Guid workloadId)
    {
        CommandDeleteWorkload request = new CommandDeleteWorkload(workloadId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }
}
