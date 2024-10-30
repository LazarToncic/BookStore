using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.LoyaltyProgram;

public class LoyaltyProgramConfiguration : IEntityTypeConfiguration<BookStore.Domain.Entities.LoyaltyProgram>
{
    public void Configure(EntityTypeBuilder<BookStore.Domain.Entities.LoyaltyProgram> builder)
    {
        builder.ToTable("LoyaltyProgram");
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.LoyaltyPoints)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasOne(lp => lp.User)
            .WithOne(u => u.LoyaltyProgram)
            .HasForeignKey<BookStore.Domain.Entities.LoyaltyProgram>(lp => lp.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}