using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.Identity;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    private const string OwnerUserId = "4DAF65CB-CC0E-4C81-9183-20097EA81F5A";
    private const string OwnerRoleId = "40FEB7B4-B530-4EA2-B96F-582D88277E4B";

    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.HasOne(ur => ur.User)
            .WithMany(r => r.Roles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        var iur = new ApplicationUserRole
        {
            RoleId = OwnerRoleId,
            UserId = OwnerUserId
        };

        builder.HasData(iur);
    }
}