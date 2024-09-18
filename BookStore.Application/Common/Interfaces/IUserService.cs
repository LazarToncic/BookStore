using BookStore.Domain.Entities;

namespace BookStore.Application.Common.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(string emailAddress, List<string> roles, string firstName, string lastName);
    Task<ApplicationUser?> GetUserAsync(string id);
    Task<ApplicationUser?> GetUserByEmailAsync(string id);
    Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);
}