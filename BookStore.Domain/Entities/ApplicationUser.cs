using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public IList<ApplicationUserRole> Roles { get; } = new List<ApplicationUserRole>();
    public IList<Order> Orders { get; set; } = new List<Order>();
    public LoyaltyProgram LoyaltyProgram { get; set; }
}