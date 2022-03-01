using WorkloadsApi.Health;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly WorkerWitness witness;

    public Worker(ILogger<Worker> logger, WorkerWitness witness)
    {
        this.logger = logger;
        this.witness = witness;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            witness.LastExecution = DateTime.Now;
            await Task.Delay(1000, stoppingToken);
        }
    }
}