namespace Countries.Db;

using Countries.Model;

using Microsoft.EntityFrameworkCore;

public interface ICountriesDbContext
{
    DbSet<Country> Countries { get; set; }
    DbSet<PhonePrefix> PhonePrefixes { get; set; }

    Task EnsureDbExists();

    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
}
