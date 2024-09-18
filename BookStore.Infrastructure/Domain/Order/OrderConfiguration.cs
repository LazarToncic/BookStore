using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.Order;

public class OrderConfiguration : IEntityTypeConfiguration<BookStore.Domain.Entities.Order>
{
    public void Configure(EntityTypeBuilder<BookStore.Domain.Entities.Order> builder)
    {
        builder.ToTable("Orders");
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.Property(o => o.OrderDate)
            .IsRequired();
        
        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(oi => oi.OrderItems)
            .WithOne(o => o.Order)
            .HasForeignKey(fk => fk.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}