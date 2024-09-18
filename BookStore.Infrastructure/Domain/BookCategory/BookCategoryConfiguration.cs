using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.BookCategory;

public class BookCategoryConfiguration : IEntityTypeConfiguration<BookStore.Domain.Entities.BookCategory>
{
    public void Configure(EntityTypeBuilder<BookStore.Domain.Entities.BookCategory> builder)
    {
        builder.ToTable("BookCategory");
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(k => k.BookCategoryName).IsRequired().HasMaxLength(20);

        builder.HasMany(x => x.BookCategoryBooks)
            .WithOne(x => x.BookCategory)
            .HasForeignKey(fk => fk.BookCategoryId);
    }
}