### Changes in project WorkloadsApi
  
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
  }
```
#### Program.cs:
builder.Services.Configure<NatsProducer>(options => builder.Configuration.GetSection("NATS").Bind(options));
NatsProducer natsProducer = builder.Configuration.GetSection("NATS").Get<NatsProducer>();
builder.Services.AddNatsClient(options =>
{
    options.Servers = natsProducer.Servers;
    options.Url = natsProducer.Url;
    options.Verbose = true;
});

#### Controllers
Add member variables
```
private readonly NatsProducer natsProducer;
private readonly IConnection connection;
private IJetStream? jetStream = null;
```
Inject into constructor
```
IOptions<NatsProducer> options, IConnection connection
```
Initialize in constructor
```
natsProducer = options.Value;
this.connection = connection;
JsUtils.CreateStreamOrUpdateSubjects(connection, natsProducer.Stream, natsProducer.Subject);
jetStream = connection.CreateJetStreamContext();
```
In controller methods
```
byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize("Your data"));
Msg msg = new Msg(natsProducer.Subject, null, null, data);
PublishAck pa = await jetStream.PublishAsync(msg);
```