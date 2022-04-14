namespace Workloads.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class WorkloadsDbContextFactory : IDesignTimeDbContextFactory<WorkloadsDbContext>
{
    private static readonly string connectionString = @"Server=localhost;User=root;Password=password;Database=workloads";

    public WorkloadsDbContext CreateDbContext(string[] args)
    {
        MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(5, 6, 51));

        DbContextOptionsBuilder<WorkloadsDbContext> optionsBuilder = new DbContextOptionsBuilder<WorkloadsDbContext>();
        optionsBuilder.UseMySql(connectionString, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        return new WorkloadsDbContext(optionsBuilder.Options);
    }
}