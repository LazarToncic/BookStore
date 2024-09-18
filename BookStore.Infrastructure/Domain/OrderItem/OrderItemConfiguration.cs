using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.OrderItem;

public class OrderItemConfiguration : IEntityTypeConfiguration<BookStore.Domain.Entities.OrderItem>
{
    public void Configure(EntityTypeBuilder<BookStore.Domain.Entities.OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.BookId).IsRequired();
        builder.Property(x => x.BookName).IsRequired();

        builder.HasOne(x => x.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(fk => fk.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}