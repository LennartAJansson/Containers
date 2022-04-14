namespace BuildVersion.Data
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class BuildVersionsDbContextFactory : IDesignTimeDbContextFactory<BuildVersionsDb>
    {
        private static readonly string connectionString = "Server=localhost;User=root;Password=password;Database=BuildVersions";

        public BuildVersionsDb CreateDbContext(string[] args)
        {
            MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(5, 6, 51));

            DbContextOptionsBuilder<BuildVersionsDb> optionsBuilder = new DbContextOptionsBuilder<BuildVersionsDb>();
            optionsBuilder.UseMySql(connectionString, serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            return new BuildVersionsDb(optionsBuilder.Options);
        }
    }
}
