namespace BuildVersion.Data
{
    public class LogEntry : BaseLoggedEntity
    {
        public int Id { get; set; }
        public string JsonBefore { get; set; }
        public string JsonAfter { get; set; }
    }
}
