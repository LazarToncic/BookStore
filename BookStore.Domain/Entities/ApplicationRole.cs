using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public IList<ApplicationUserRole> UserRoles { get; set; }
}