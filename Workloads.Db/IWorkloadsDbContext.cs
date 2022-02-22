namespace Workloads.Db
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Workloads.Model;

    public interface IWorkloadsDbContext
    {
        DbSet<Assignment> Assignments { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<Workload> Workloads { get; set; }

        Task EnsureDbExists();

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}