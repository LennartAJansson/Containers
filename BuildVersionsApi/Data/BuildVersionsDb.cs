namespace BuildVersionsApi.Data
{

    using System;
    using System.Threading;

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
            _ = optionsBuilder.UseLoggerFactory(loggerFactory);
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

        public override int SaveChanges()
        {
            PreSaveChanges();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            PreSaveChanges();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            PreSaveChanges();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            PreSaveChanges();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void PreSaveChanges()
        {
            foreach (BaseLoggedEntity? history in ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseLoggedEntity)
                .Select(e => e.Entity as BaseLoggedEntity))
            {
                history!.Changed = DateTime.Now;
                if (history.Created == DateTime.MinValue)
                {
                    history.Created = DateTime.Now;
                }
            }

            //foreach (BaseLoggedEntity history in ChangeTracker
            //    .Entries()
            //    .Where(e => e.Entity is BaseLoggedEntity && (e.State == EntityState.Added))
            //    .Select(e => e.Entity as BaseLoggedEntity))
            //{
            //    LogEntry.Add(new LogEntry
            //    {
            //        JsonBefore = "",
            //        JsonAfter = JsonSerializer.Serialize(history),
            //        Changed = DateTime.Now,
            //        Created = DateTime.Now
            //    });
            //}

            //foreach (var history in ChangeTracker
            //    .Entries()
            //    .Where(e => e.Entity is BaseLoggedEntity && (e.State == EntityState.Modified))
            //    .Select(e => new { Old = e.OriginalValues, New = e.Entity as BaseLoggedEntity }))
            //{
            //    history.New.Changed = DateTime.Now;

            //    if (history.New.Created == DateTime.MinValue)
            //    {
            //        history.New.Created = DateTime.Now;
            //        LogEntry.Add(new LogEntry
            //        {
            //            JsonBefore = "",
            //            JsonAfter = JsonSerializer.Serialize(history),
            //            Changed = DateTime.Now,
            //            Created = DateTime.Now
            //        });
            //    }
            //    else
            //    {
            //        LogEntry.Add(new LogEntry
            //        {
            //            JsonBefore = JsonSerializer.Serialize(history.Old.ToObject()),
            //            JsonAfter = JsonSerializer.Serialize(history),
            //            Changed = DateTime.Now,
            //            Created = DateTime.Now
            //        });
            //    }

            //}
        }
    }
}
