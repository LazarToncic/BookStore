using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Common.Interfaces;

public interface IDemoDbContext
{
    public DbSet<Domain.Entities.Book> Book { get; }
    public DbSet<Domain.Entities.BookCategory> BookCategory { get; }
    public DbSet<BookCategoryBook> BookCategoryBook { get; }
    public DbSet<Domain.Entities.Order> Order { get; }
    public DbSet<OrderItem> OrderItem { get; }
    public DbSet<ApplicationUser> Users { get; }
    public DbSet<ApplicationRole> Roles { get; }
    public DbSet<Domain.Entities.Author> Authors { get; }
    public DbSet<AuthorsBooks> AuthorsBooks { get; }
    public DbSet<Domain.Entities.LoyaltyProgram> LoyaltyPrograms { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}