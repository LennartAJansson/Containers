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
        => this.logger = logger;

    //People
    [HttpGet]
    public async Task<IActionResult> GetPeopleAsync()
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        IEnumerable<QueryPersonResponse>? result = await mediator.Send(new QueryPeople());
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpGet("{personId}")]
    public async Task<IActionResult> GetPersonAsync(Guid personId)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        QueryPersonResponse? result = await mediator.Send(new QueryPerson(personId));
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    //Assignments
    [HttpGet]
    public async Task<IActionResult> GetAssignmentsAsync()
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        IEnumerable<QueryAssignmentResponse>? result = await mediator.Send(new QueryAssignments());
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpGet("{assignmentId}")]
    public async Task<IActionResult> GetAssignmentAsync(Guid assignmentId)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        QueryAssignmentResponse? result = await mediator.Send(new QueryAssignment(assignmentId));
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    //Workloads
    [HttpGet]
    public async Task<IActionResult> GetWorkloadsAsync()
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        IEnumerable<QueryWorkloadResponse>? result = await mediator.Send(new QueryWorkloads());
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }

    [HttpGet("{workloadId}")]
    public async Task<IActionResult> GetWorkloadAsync(Guid workloadId)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");
        DateTime startDateTime = DateTime.Now;
        QueryWorkloadResponse? result = await mediator.Send(new QueryWorkload(workloadId));
        DateTime endDateTime = DateTime.Now;

        RequestExecuteTime.Labels(Request.Path).Set((endDateTime - startDateTime).TotalMilliseconds);
        Counter.Labels(Request.Path).Inc();

        return Ok(result);
    }
}
