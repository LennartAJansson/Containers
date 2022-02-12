namespace Workloads.Model;

public class Assignment
{
    public Guid AssignmentId { get; set; }
    public string? CustomerName { get; set; }
    public string? Description { get; set; }

    //Browsing property:
    public virtual ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
}
