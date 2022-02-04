namespace Workloads.Model
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }

        //Browsing property:
        public virtual ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
    }
}