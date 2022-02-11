namespace WorkloadsApi.Mediators.Commands
{
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

    public class CreateAssignmentMediator : IRequestHandler<CommandCreateAssignment, CommandAssignmentResponse>
    {
        private readonly ILogger<CreateAssignmentMediator> logger;
        private readonly IConnection connection;
        private readonly NatsProducer natsProducer;
        private readonly IJetStream? jetStream = null;

        public CreateAssignmentMediator(ILogger<CreateAssignmentMediator> logger, IConnection connection, IOptions<NatsProducer> options)
        {
            this.logger = logger;
            connection = connection;
            natsProducer = options.Value;

            JetStreamUtils.CreateStreamOrUpdateSubjects(connection, natsProducer.Stream, natsProducer.Subject);
            jetStream = connection.CreateJetStreamContext();
        }

        public async Task<CommandAssignmentResponse> Handle(CommandCreateAssignment request, CancellationToken cancellationToken)
        {
            CloudEvent evt = new CloudEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Data = request,
                DataContentType = "application/json",
                Type = request.GetType().FullName,
                Source = new Uri(GetType().FullName),
                Subject = natsProducer.Subject
            };
            //TODO Handle Correlation, make AssignmentId's to Guid and create them in advance
            JsonSerializerOptions? options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            byte[] data = JsonSerializer.SerializeToUtf8Bytes(evt, options);

            Msg msg = new Msg(natsProducer.Subject, null, null, data);

            string responseText = "Failed";

            if (jetStream != null)
            {
                PublishAck pa = await jetStream.PublishAsync(msg);

                responseText = $"Published message {Encoding.UTF8.GetString(data)} on subject {natsProducer.Subject}, stream {pa.Stream}, seqno {pa.Seq}.";
                logger.LogInformation(responseText);
            }

            return new CommandAssignmentResponse(1, responseText);
        }
    }

}