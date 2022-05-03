namespace BuildVersionsApi.Data
{
    public class Binary : BaseLoggedEntity
    {
        public int Id { get; set; }
        public string ProjectFile { get; set; } = "";
        public int BuildVersionId { get; set; }
        public virtual BuildVersion BuildVersion { get; set; }
    }
}
