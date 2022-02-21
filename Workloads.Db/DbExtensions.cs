namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Workloads.Db;
using Workloads.Model;

public static class DbExtensions
{
    public static IServiceCollection AddWorkloadsDb(this IServiceCollection services, IConfiguration configuration)
    {
        MySqlServerVersion? serverVersion = new MySqlServerVersion(new Version(5, 6, 51));//5.6.51

        services.Configure<ConnectionStrings>(c => configuration.GetSection("ConnectionStrings").Bind(c));
        ConnectionStrings connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

        services.AddDbContext<IWorkloadsDbContext, WorkloadsDbContext>(options =>
             options.UseMySql(connectionStrings.WorkloadsDb, serverVersion)
                 .EnableSensitiveDataLogging()
                 .EnableDetailedErrors(),
            ServiceLifetime.Transient, ServiceLifetime.Transient);

        services.AddTransient<IWorkloadsService, WorkloadsService>();

        return services;
    }

    public static IHost UpdateDb(this IHost host)
    {
        host.Services.GetRequiredService<WorkloadsDbContext>().EnsureDbExists();

        return host;
    }
}
