namespace Countries.Db;

using Countries.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class DbExtensions
{
    public static IServiceCollection AddCountriesDb(this IServiceCollection services, IConfiguration configuration)
    {
        MySqlServerVersion? serverVersion = new MySqlServerVersion(new Version(5, 6, 51));//5.6.51

        services.Configure<ConnectionStrings>(c => configuration.GetSection("ConnectionStrings").Bind(c));
        ConnectionStrings connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

        services.AddDbContext<ICountriesDbContext, CountriesDbContext>(options =>
             options.UseMySql(connectionStrings.CountriesDb, serverVersion)
                 .EnableSensitiveDataLogging()
                 .EnableDetailedErrors(),
            ServiceLifetime.Transient, ServiceLifetime.Transient);

        services.AddTransient<ICountriesService, CountriesService>();

        return services;
    }

    public static IHost UpdateDb(this IHost host)
    {
        host.Services.GetRequiredService<CountriesDbContext>().EnsureDbExists();

        return host;
    }
}
