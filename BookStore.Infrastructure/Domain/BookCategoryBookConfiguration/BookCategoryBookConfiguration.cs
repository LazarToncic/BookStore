using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.BookCategoryBookConfiguration;

public class BookCategoryBookConfiguration : IEntityTypeConfiguration<BookCategoryBook>
{
    public void Configure(EntityTypeBuilder<BookCategoryBook> builder)
    {
        builder.ToTable("Book_Category");
        builder.HasKey(key => new { key.BookId, key.BookCategoryId });

        builder.HasOne(x => x.Book)
            .WithMany(x => x.BookCategoryBooks)
            .HasForeignKey(fk => fk.BookId);

        builder.HasOne(x => x.BookCategory)
            .WithMany(x => x.BookCategoryBooks)
            .HasForeignKey(fk => fk.BookCategoryId);
    }
}