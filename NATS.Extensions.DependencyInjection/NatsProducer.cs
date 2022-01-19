namespace NATS.Extensions.DependencyInjection
{
    public class NatsProducer
    {
        public string[]? Servers { get; set; }
        public string? Url { get; set; }
        public string? Stream { get; set; }
        public string? Subject { get; set; }
    }
}