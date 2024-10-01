using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain;

public class AuthorsBooksConfiguration : IEntityTypeConfiguration<AuthorsBooks>
{
    public void Configure(EntityTypeBuilder<AuthorsBooks> builder)
    {
        builder.ToTable("AuthorsBooks");
        builder.HasKey(x => new { x.BookId, x.AuthorId });

        builder.HasOne(x => x.Book)
            .WithMany(x => x.AuthorsBooks)
            .HasForeignKey(x => x.BookId);

        builder.HasOne(x => x.Author)
            .WithMany(x => x.AuthorsBooks)
            .HasForeignKey(x => x.AuthorId);
    }
}