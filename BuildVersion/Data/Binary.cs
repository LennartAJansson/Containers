namespace BuildVersion.Data
{
    public class Binary
    {
        public int Id { get; set; }
        public string ProjectFile { get; set; } = "";
        public int BuildVersionId { get; set; }
        public virtual BuildVersion? BuildVersion { get; set; }
    }
}
