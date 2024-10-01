using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.Author;

public class AuthorConfiguration : IEntityTypeConfiguration<BookStore.Domain.Entities.Author>
{
    public void Configure(EntityTypeBuilder<BookStore.Domain.Entities.Author> builder)
    {
        builder.ToTable("Author");
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(20);
        builder.Property(x => x.YearOfBirth).IsRequired();

        builder.HasMany(x => x.AuthorsBooks)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId);
    }
}