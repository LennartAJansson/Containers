namespace Countries.Db;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class CountriesDbContextFactory : IDesignTimeDbContextFactory<CountriesDbContext>
{
    private static readonly string connectionString = @"Server=localhost;User=root;Password=password;Database=countries";

    public CountriesDbContext CreateDbContext(string[] args)
    {
        MySqlServerVersion? serverVersion = new MySqlServerVersion(new Version(5, 6, 51));

        DbContextOptionsBuilder<CountriesDbContext> optionsBuilder = new DbContextOptionsBuilder<CountriesDbContext>();
        optionsBuilder.UseMySql(connectionString, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        return new CountriesDbContext(optionsBuilder.Options);
    }
}