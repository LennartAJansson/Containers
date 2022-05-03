namespace BuildVersionsApi.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class BaseLoggedEntity
    {
        [NotMapped]
        public bool IsDirty { get; set; }

        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
    }
}
