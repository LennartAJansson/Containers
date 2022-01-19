### Changes in project WorkloadsProjector
  
#### Project change:  
- Add project reference to NATS.Extensions.DependencyInjection  
- Menu option: Add - Docker Support - Linux  
- Menu option: Add - Container Orchestration Support - Docker Compose  
- Modify Docker-Compose.yml and Docker-Compose.override.yml according to AddToDockerCompose.md  
  
#### appsettings.json:  
```
  "NATS": {
    "Servers": [
      "nats://localhost:4222"
    ],
    "Url": "nats://localhost:4222",
    "Stream": "workloads",
    "Subject": "reported-workloads",
    "Consumer": "projector",
    "DeliverySubject": "projector-delivery"
  }
```

#### Program.cs:
```
.ConfigureServices((context,services) =>
{
    services.Configure<NatsProducer>(options => context.Configuration.GetSection("NATS").Bind(options));
    NatsProducer natsProducer = context.Configuration.GetSection("NATS").Get<NatsProducer>();
    services.AddNatsClient(options =>
    {
        options.Servers = natsProducer.Servers;
        options.Url = natsProducer.Url;
        options.Verbose = true;
    });
    services.AddHostedService<Worker>();
})
```

#### Worker.cs:
Add member variables
```
private readonly NatsConsumer natsConsumer;
private readonly IConnection connection;
private IJetStream? jetStream = null;
private IJetStreamPushAsyncSubscription? subscription = null;
```
Inject and initialize
```
public Worker(ILogger<Worker> logger, IOptions<NatsConsumer> options, IConnection connection)
{
    this.logger = logger;
    this.connection = connection;
    natsConsumer = options.Value;
    JsUtils.CreateStreamWhenDoesNotExist(connection, natsConsumer.Stream, natsConsumer.Subject);
}
```
Override StartAsync
```
public override Task StartAsync(CancellationToken cancellationToken)
{
    ConsumerConfiguration cc = ConsumerConfiguration.Builder()
                            .WithDurable(natsConsumer.Consumer)
                            .WithDeliverSubject(natsConsumer.DeliverySubject)
                            .Build();

    connection.CreateJetStreamManagementContext()
        .AddOrUpdateConsumer(natsConsumer.Stream, cc);

    jetStream = connection.CreateJetStreamContext();

    PushSubscribeOptions so = PushSubscribeOptions.BindTo(natsConsumer.Stream, natsConsumer.Consumer);
    subscription = jetStream.PushSubscribeAsync(natsConsumer.Subject, MessageArrived, true, so);

    return base.StartAsync(cancellationToken);
}
```
Create callback
```
private void MessageArrived(object? sender, MsgHandlerEventArgs args)
{
    Msg msg = args.Message;
    msg.Ack();

    logger.LogInformation("Received message {data} on subject {subject}, stream {stream}, seqno {seqno}.",
        Encoding.UTF8.GetString(msg.Data), natsConsumer.Subject, msg.MetaData.Stream, msg.MetaData.StreamSequence);
}
```