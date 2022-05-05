namespace WorkloadsApi.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Workloads.Contract;

[Route("api/[controller]/[action]")]
[ApiController]
public class QueryController : MetricsControllerBase
{
    private readonly ILogger<QueryController> logger;

    public QueryController(ILogger<QueryController> logger, IMediator mediator)
        : base(mediator)
    {
        this.logger = logger;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QueryPersonResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> GetPeopleAsync()
    {
        QueryPeople request = new QueryPeople();
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        IEnumerable<QueryPersonResponse> result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueryPersonResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{personId}")]
    public async Task<IActionResult> GetPersonAsync(Guid personId)
    {
        QueryPerson request = new QueryPerson(personId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        QueryPersonResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QueryAssignmentResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> GetAssignmentsAsync()
    {
        QueryAssignments request = new QueryAssignments();
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        IEnumerable<QueryAssignmentResponse> result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueryAssignmentResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{assignmentId}")]
    public async Task<IActionResult> GetAssignmentAsync(Guid assignmentId)
    {
        QueryAssignment request = new QueryAssignment(assignmentId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        QueryAssignmentResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QueryWorkloadResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> GetWorkloadsAsync()
    {
        QueryWorkloads request = new QueryWorkloads();
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        IEnumerable<QueryWorkloadResponse> result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueryWorkloadResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{workloadId}")]
    public async Task<IActionResult> GetWorkloadAsync(Guid workloadId)
    {
        QueryWorkload request = new QueryWorkload(workloadId);
        logger.LogDebug("Received {type}", request.GetType().Name);

        DateTime startDateTime = DateTime.Now;
        QueryWorkloadResponse result = await mediator.Send(request);
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }
}
