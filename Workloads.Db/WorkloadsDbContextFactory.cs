namespace Workloads.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class WorkloadsDbContextFactory : IDesignTimeDbContextFactory<WorkloadsDbContext>
{
    private static readonly string connectionString =
        @"Server=mysql.mysql.svc;User=root;Password=password;Database=workloads";
    //@"Server=localhost;User=root;Password=password;Database=workloads";
    //@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=workloadsdb;Integrated Security=True";
    public WorkloadsDbContext CreateDbContext(string[] args)
    {
        MySqlServerVersion? serverVersion = new MySqlServerVersion(new Version(5, 6, 51));//5.6.51

        DbContextOptionsBuilder<WorkloadsDbContext> optionsBuilder = new DbContextOptionsBuilder<WorkloadsDbContext>();
        optionsBuilder.UseMySql(connectionString, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
        //optionsBuilder.UseSqlServer(connectionString);

        return new WorkloadsDbContext(optionsBuilder.Options);
    }
}