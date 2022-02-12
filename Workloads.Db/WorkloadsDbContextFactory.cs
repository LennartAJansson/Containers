namespace Workloads.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class WorkloadsDbContextFactory : IDesignTimeDbContextFactory<WorkloadsDbContext>
{
    private static readonly string connectionString =
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=workloadsdb;Integrated Security=True";
    public WorkloadsDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<WorkloadsDbContext> optionsBuilder = new DbContextOptionsBuilder<WorkloadsDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new WorkloadsDbContext(optionsBuilder.Options);
    }
}