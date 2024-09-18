using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.Book;

public class BookConfiguration : IEntityTypeConfiguration<BookStore.Domain.Entities.Book>
{
    public void Configure(EntityTypeBuilder<BookStore.Domain.Entities.Book> builder)
    {
        builder.ToTable("Book");
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(k => k.Name).IsRequired().HasMaxLength(30);

        builder.HasMany(x => x.BookCategoryBooks)
            .WithOne(k => k.Book)
            .HasForeignKey(x => x.BookId);
    }
}