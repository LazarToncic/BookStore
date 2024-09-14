using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Domain.Identity;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    private const string OwnerId = "40FEB7B4-B530-4EA2-B96F-582D88277E4B";
    private const string StoreManagerId = "891E6770-C605-4D45-B959-8906C5728935";
    private const string EmployeeId = "3B690889-501F-4A19-B2F7-3C55F6D8BAC9";
    private const string CustomerId = "7B496879-107T-4B29-G5FF-3C55F6D8BAC9";
    
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles");
        builder.HasData(
            new ApplicationRole
            {
                Id = OwnerId,
                Name = "Owner",
                NormalizedName = "OWNER",
                ConcurrencyStamp = "a09ab67f-02d6-4910-8659-3385759d8036",
            },
            new ApplicationRole
            {
                Id = StoreManagerId,
                Name = "StoreManager",
                NormalizedName = "STOREMANAGER",
                ConcurrencyStamp = "a09ab67f-02d6-4910-8659-3385759d8037"
            },
            new ApplicationRole
            {
                Id = EmployeeId,
                Name = "Employee",
                NormalizedName = "EMPLOYEE",
                ConcurrencyStamp = "a09ab67f-02d6-4910-8659-3385759d8038"
            },
            new ApplicationRole
            {
                Id = CustomerId,
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = "a09ab67f-02d6-4910-8659-3385759d8038"
            }
        );
    }
}