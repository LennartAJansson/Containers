namespace Workloads.Model
{
    public class Workload
    {
        public int WorkloadId { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset Stop { get; set; }
        public int AssignmentId { get; set; }//Foreign Key
        public int PersonId { get; set; }//Foreign Key

        //Browsing properties:
        public virtual Assignment Assignment { get; set; }
        public virtual Person Person { get; set; }
    }
}