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
        services.Configure<ConnectionStrings>(c => configuration.GetSection("ConnectionStrings").Bind(c));
        ConnectionStrings connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

        services.AddDbContext<WorkloadsDbContext>(options =>
            options.UseSqlServer(connectionStrings.WorkloadsDb), ServiceLifetime.Transient, ServiceLifetime.Transient);

        return services;
    }

    public static IHost UpdateDb(this IHost host)
    {
        host.Services.GetRequiredService<WorkloadsDbContext>().EnsureDbExists();

        return host;
    }
}
