namespace WorkloadsApi.Mediators.Commands;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using CloudNative.CloudEvents;

using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NATS.Client;
using NATS.Client.JetStream;
using NATS.Extensions.DependencyInjection;

using Workloads.Contract;

public class CreatePersonMediator : NatsCommandMediatorBase, IRequestHandler<CommandCreatePerson, CommandPersonResponse>
{
    private readonly ILogger<CreatePersonMediator> logger;

    public CreatePersonMediator(ILogger<CreatePersonMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        : base(connection, options.Value)
        => this.logger = logger;

    public async Task<CommandPersonResponse> Handle(CommandCreatePerson request, CancellationToken cancellationToken)
    {
        //TODO Add meaningful logging
        logger.LogDebug("");

        CommandCreatePersonWithId parsed = new(Guid.NewGuid(), request.Name);

        CloudEvent evt = new CloudEvent()
        {
            Id = parsed.PersonId.ToString(),
            Data = parsed,
            DataContentType = "application/json",
            Type = parsed.GetType().FullName,
            //Source = new Uri(GetType().FullName!),
            Subject = natsProducer.Subject //TODO Add evt.Id/parsedRequest.PersonId to Subject?
        };

        JsonSerializerOptions? options = new JsonSerializerOptions();
        options.Converters.Add(new CustomJsonConverterForType());

        byte[] data = JsonSerializer.SerializeToUtf8Bytes(evt, options);

        Msg msg = new Msg(natsProducer.Subject, null, null, data);//TODO Add evt.Id/parsedRequest.PersonId to Subject

        string responseText = "Failed";

        if (jetStream != null)
        {
            PublishAck pa = await jetStream.PublishAsync(msg);

            responseText = $"Published message {Encoding.UTF8.GetString(data)} on subject {natsProducer.Subject}, stream {pa.Stream}, seqno {pa.Seq}.";
            logger.LogInformation("{responseText}", responseText);
        }

        return new CommandPersonResponse(parsed.PersonId, responseText);
    }
}
