using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WorkloadsProjector.Health
{
    internal class ProjectorHealthCheck : IHealthCheck
    {
        private readonly ILogger<ProjectorHealthCheck> logger;
        private readonly WorkerWitness witness;

        public ProjectorHealthCheck(ILogger<ProjectorHealthCheck> logger, WorkerWitness witness)
        {
            this.logger = logger;
            this.witness = witness;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            //TODO Add meaningful logging
            logger.LogDebug("");
            return DateTime.Now.Subtract(witness.LastExecution).TotalSeconds < 120 ?
                           Task.FromResult(HealthCheckResult.Healthy("I'm working my butt off")) :
                           Task.FromResult(HealthCheckResult.Unhealthy("I feel a bit exhausted"));
        }
    }
}