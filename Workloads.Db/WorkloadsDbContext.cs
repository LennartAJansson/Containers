namespace Workloads.Db;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Workloads.Model;

public class WorkloadsDbContext : DbContext, IWorkloadsDbContext
{
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Workload> Workloads { get; set; }
    public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    private ILogger<WorkloadsDbContext> logger;

    public WorkloadsDbContext(DbContextOptions<WorkloadsDbContext> options)
            : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Account>().HasKey(t => t.AccountId);
        //modelBuilder.Entity<Office>().HasKey(t => t.OfficeId);
        //modelBuilder.Entity<Occasion>().HasKey(t => t.OccasionId);
        //modelBuilder.Entity<Activity>().HasKey(t => t.ActivityId);
        //modelBuilder.Entity<ActivityType>().HasKey(t => t.ActivityTypeId);
        //modelBuilder.Entity<Occasion>().HasOne<Account>().WithMany().HasForeignKey(o => o.AccountId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Occasions_Accounts_AccountId");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(loggerFactory);
        logger = loggerFactory.CreateLogger<WorkloadsDbContext>();
    }

    public Task EnsureDbExists()
    {
        IEnumerable<string> migrations = Database.GetPendingMigrations();
        if (migrations.Any())
        {
            logger.LogInformation("Adding {count} migrations", migrations.Count());
            Database.Migrate();
        }
        else
        {
            logger.LogInformation("Migrations up to date");
        }

        return Task.CompletedTask;
    }
}
