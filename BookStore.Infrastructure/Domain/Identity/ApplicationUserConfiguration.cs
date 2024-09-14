using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.Identity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    private const string OwnerId = "4DAF65CB-CC0E-4C81-9183-20097EA81F5A";
    
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        
        var admin = new ApplicationUser
        {
            Id = OwnerId,
            UserName = "ltoncic@gmail.com",
            NormalizedUserName = "LTONCIC@GMAIL.COM",
            Email = "ltoncic@gmail.com",
            NormalizedEmail = "LTONCIC@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = new Guid().ToString("D"),
            FirstName = "Lazar",
            LastName = "Toncic",
            ConcurrencyStamp = "c188a435-cfc8-45fd-836c-9a18bb9de405",
            AccessFailedCount = 0
        };

        builder.HasData(admin);

        builder.HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}