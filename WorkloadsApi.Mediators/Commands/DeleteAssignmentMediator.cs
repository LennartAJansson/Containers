namespace WorkloadsApi.Mediators.Commands;

using CloudNative.CloudEvents;

using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NATS.Client;
using NATS.Client.JetStream;
using NATS.Extensions.DependencyInjection;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Workloads.Contract;

public class DeleteAssignmentMediator : NatsCommandMediatorBase, IRequestHandler<CommandDeleteAssignment, CommandAssignmentResponse>
{
    private readonly ILogger<DeleteAssignmentMediator> logger;

    public DeleteAssignmentMediator(ILogger<DeleteAssignmentMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        : base(connection, options.Value)
    {
        this.logger = logger;
    }

    public async Task<CommandAssignmentResponse> Handle(CommandDeleteAssignment request, CancellationToken cancellationToken)
    {
        logger.LogDebug("Creating and sending message to NATS for {type}", request.GetType().Name);

        CloudEvent evt = new CloudEvent()
        {
            Id = request.AssignmentId.ToString(),
            Data = request,
            DataContentType = "application/json",
            Type = request.GetType().FullName,
            //Source = new Uri(GetType().FullName!),
            Subject = natsProducer.Subject //TODO Add evt.Id/request.AssignmentId to Subject?
        };

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new CustomJsonConverterForType());

        byte[] data = JsonSerializer.SerializeToUtf8Bytes(evt, options);

        Msg msg = new Msg(natsProducer.Subject, null, null, data);//TODO Add evt.Id/request.AssignmentId to Subject

        string responseText = "Failed";

        if (jetStream != null)
        {
            PublishAck pa = await jetStream.PublishAsync(msg);

            responseText = $"Published message {Encoding.UTF8.GetString(data)} on subject {natsProducer.Subject}, stream {pa.Stream}, seqno {pa.Seq}.";
            logger.LogInformation("{responseText}", responseText);
        }

        return new CommandAssignmentResponse(request.AssignmentId, responseText);
    }
}
