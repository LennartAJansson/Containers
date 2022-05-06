namespace WorkloadsApi.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Prometheus;

public class MetricsControllerBase<T> : ControllerBase
{
    protected readonly ILogger<T> logger;

    private static Gauge requestExecuteTime = Metrics.CreateGauge("workloadsapi_controllers_executiontime", "Counts total execution time for handling requests",
        new GaugeConfiguration
        {
            LabelNames = new[] { "path" }
        });

    private static Counter counter = Metrics.CreateCounter("workloadsapi_controllers_counter", "Counts total calls for handling requests",
        new CounterConfiguration
        {
            LabelNames = new[] { "path" }
        });
    protected IMediator mediator;

    public MetricsControllerBase(ILogger<T> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    protected void SetMetrics(double ms)
    {
        RequestExecuteTime.Labels(Request.Path).Set(ms);
        Counter.Labels(Request.Path).Inc();
    }

    protected static Gauge RequestExecuteTime { get => requestExecuteTime; set => requestExecuteTime = value; }
    protected static Counter Counter { get => counter; set => counter = value; }
}
