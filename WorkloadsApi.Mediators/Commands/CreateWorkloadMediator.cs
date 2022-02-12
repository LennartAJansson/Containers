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

public class CreateWorkloadMediator : NatsCommandMediatorBase, IRequestHandler<CommandCreateWorkload, CommandWorkloadResponse>
{
    private readonly ILogger<CreateWorkloadMediator> logger;

    public CreateWorkloadMediator(ILogger<CreateWorkloadMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        : base(connection, options.Value)
        => this.logger = logger;

    public async Task<CommandWorkloadResponse> Handle(CommandCreateWorkload request, CancellationToken cancellationToken)
    {
        CommandCreateWorkloadWithId parsed = new(Guid.NewGuid(), request.Start, request.Stop, request.PersonId, request.AssignmentId);

        CloudEvent evt = new CloudEvent()
        {
            Id = parsed.WorkloadId.ToString(),//This will be the Guid for the Workload, to enable tracking
            Data = parsed,
            DataContentType = "application/json",
            Type = parsed.GetType().FullName,
            Source = new Uri(GetType().FullName!),
            Subject = natsProducer.Subject //Add evt.Id/parsedRequest.WorkloadId to Subject?
        };

        JsonSerializerOptions? options = new JsonSerializerOptions();
        options.Converters.Add(new CustomJsonConverterForType());

        byte[] data = JsonSerializer.SerializeToUtf8Bytes(evt, options);

        Msg msg = new Msg(natsProducer.Subject, null, null, data);//TODO Add evt.Id/parsedRequest.WorkloadId to Subject

        string responseText = "Failed";

        if (jetStream != null)
        {
            PublishAck pa = await jetStream.PublishAsync(msg);

            responseText = $"Published message {Encoding.UTF8.GetString(data)} on subject {natsProducer.Subject}, stream {pa.Stream}, seqno {pa.Seq}.";
            logger.LogInformation("{responseText}", responseText);
        }

        return new CommandWorkloadResponse(parsed.WorkloadId, responseText);
    }
}
