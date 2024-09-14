using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Common.Interfaces;

public interface IDemoDbContext
{
    //public DbSet<Product> Products { get; }
    //public DbSet<Company> Companies { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}