namespace WorkloadsApi.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Workloads.Contract;

[Route("api/[controller]/[action]")]
[ApiController]
public class CommandController : MetricsControllerBase<CommandController>
{
    public CommandController(ILogger<CommandController> logger, IMediator mediator)
        : base(logger, mediator)
    { }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandPersonResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> CreatePersonAsync([FromBody] CommandCreatePerson request)
    {
        logger.LogInformation("Received {name}", request.GetType().Name);
        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandPersonResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<IActionResult> UpdatePersonAsync([FromBody] CommandUpdatePerson request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandPersonResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{personId}")]
    public async Task<IActionResult> DeletePersonAsync(Guid personId)
    {
        CommandDeletePerson request = new CommandDeletePerson(personId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandPersonResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandAssignmentResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> CreateAssignmentAsync([FromBody] CommandCreateAssignment request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandAssignmentResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<IActionResult> UpdateAssignmentAsync([FromBody] CommandUpdateAssignment request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandAssignmentResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{assignmentId}")]
    public async Task<IActionResult> DeleteAssignmentAsync(Guid assignmentId)
    {
        CommandDeleteAssignment request = new CommandDeleteAssignment(assignmentId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandAssignmentResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandWorkloadResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> CreateWorkloadAsync([FromBody] CommandCreateWorkload request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandWorkloadResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<IActionResult> UpdateWorkloadAsync([FromBody] CommandUpdateWorkload request)
    {
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommandWorkloadResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{workloadId}")]
    public async Task<IActionResult> DeleteWorkloadAsync(Guid workloadId)
    {
        CommandDeleteWorkload request = new CommandDeleteWorkload(workloadId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        CommandWorkloadResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        SetMetrics((endDateTime - startDateTime).TotalMilliseconds);

        return Ok(result);
    }
}
