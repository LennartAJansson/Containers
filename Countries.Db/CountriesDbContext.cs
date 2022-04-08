namespace Countries.Db;

using Countries.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class CountriesDbContext : DbContext, ICountriesDbContext
{
    public DbSet<Country> Countries { get; set; }

    public DbSet<PhonePrefix> PhonePrefixes { get; set; }

    public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    private ILogger<CountriesDbContext> logger;

    public CountriesDbContext(DbContextOptions options)
        : base(options) { }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().Property(p => p.CountryId).ValueGeneratedNever();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(loggerFactory);
        logger = loggerFactory.CreateLogger<CountriesDbContext>();
    }
}
