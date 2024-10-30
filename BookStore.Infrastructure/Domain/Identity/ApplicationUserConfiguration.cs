using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.Identity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(u => u.LoyaltyProgram)
            .WithOne(lp => lp.User)
            .HasForeignKey<BookStore.Domain.Entities.LoyaltyProgram>(lp => lp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}