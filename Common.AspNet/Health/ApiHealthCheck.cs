namespace Common.AspNet.Health;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

internal class ApiHealthCheck : IHealthCheck
{
    //TODO Implement WorkerWitness somehow
    private readonly ILogger<ApiHealthCheck> logger;
    private readonly WorkerWitness witness;

    public ApiHealthCheck(ILogger<ApiHealthCheck> logger, WorkerWitness witness)
    {
        this.logger = logger;
        this.witness = witness;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Checking Health...");
        return DateTime.Now.Subtract(witness.LastExecution).TotalSeconds < 120 ?
                       Task.FromResult(HealthCheckResult.Healthy("I'm working my butt off")) :
                       Task.FromResult(HealthCheckResult.Unhealthy("I feel a bit exhausted"));

        //bool healthCheckResultHealthy = true;//Change to some class that tests health on all systems

        //if (healthCheckResultHealthy)
        //{
        //    logger.LogDebug("Healthy");
        //    return Task.FromResult(HealthCheckResult.Healthy("The check indicates a healthy result."));
        //}

        ////Also supports HealthCheckResult.Degraded

        //logger.LogWarning("The check indicates an unhealthy result.");

        //return Task.FromResult(HealthCheckResult.Unhealthy("The check indicates an unhealthy result."));
    }
}
