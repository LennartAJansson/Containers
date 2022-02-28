namespace BuildVersion
{
    using Microsoft.EntityFrameworkCore;

    public class BuildVersionsDb : DbContext
    {
        public DbSet<Binary>? Binaries { get; set; }
        public DbSet<BuildVersion>? BuildVersions { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        private ILogger<BuildVersionsDb>? logger;
        public BuildVersionsDb(DbContextOptions<BuildVersionsDb> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<Binary>().HasIndex("ProjectFile");


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            logger = loggerFactory.CreateLogger<BuildVersionsDb>();
        }

        public Task EnsureDbExists()
        {
            IEnumerable<string> migrations = Database.GetPendingMigrations();
            if (migrations.Any())
            {
                logger?.LogInformation("Adding {count} migrations", migrations.Count());
                Database.Migrate();
            }
            else
            {
                logger?.LogInformation("Migrations up to date");
            }

            return Task.CompletedTask;
        }
    }
}
