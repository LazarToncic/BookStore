using System.Reflection;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Context;

public class DemoDbContext(DbContextOptions<DemoDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, 
    ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>(options), IDemoDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=root;Database=BookStore");
    }


    public DbSet<Book> Book => Set<Book>();
    public DbSet<BookCategory> BookCategory => Set<BookCategory>();
    public DbSet<BookCategoryBook> BookCategoryBook => Set<BookCategoryBook>();
    public DbSet<Order> Order => Set<Order>();
    public DbSet<OrderItem> OrderItem => Set<OrderItem>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<AuthorsBooks> AuthorsBooks => Set<AuthorsBooks>();
}