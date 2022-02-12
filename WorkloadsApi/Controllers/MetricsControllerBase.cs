namespace WorkloadsApi.Controllers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Prometheus;

public class MetricsControllerBase : ControllerBase
{
    private static Gauge requestExecuteTime = Metrics.CreateGauge("createperson_executiontime", "Counts total execution time for sending requests",
        new GaugeConfiguration
        {
            LabelNames = new[] { "time" }
        });

    private static Counter counter = Metrics.CreateCounter("createperson_counter", "",
        new CounterConfiguration
        {
            LabelNames = new[] { "requests" }
        });
    protected IMediator mediator;

    public MetricsControllerBase(IMediator mediator) => this.mediator = mediator;

    protected static Gauge RequestExecuteTime { get => requestExecuteTime; set => requestExecuteTime = value; }
    protected static Counter Counter { get => counter; set => counter = value; }

}
