using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Exceptions;

namespace BookStore.Infrastructure.Identity;

public class UserService(ApplicationUserManager userManager) : IUserService
{
    public async Task CreateUserAsync(string emailAddress, List<string> roles, string firstName, string lastName)
    {
        var alreadyExists = await userManager.FindByEmailAsync(emailAddress);

        if (alreadyExists != null)
            return;

        var user = new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            Email = emailAddress,
            UserName = emailAddress
        };

        try
        {
            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new AuthException("Could not create new user",
                    new {Errors = result.Errors.ToList()});
            }

            var rolesResult = await userManager.AddToRolesAsync(user,
                roles.Select(nr => nr.ToUpper()));

            if (!rolesResult.Succeeded)
            {
                await userManager.DeleteAsync(user);

                throw new AuthException("Could not add roles to the user",
                    new {Errors = rolesResult.Errors.ToList()});
            }
        }
        catch (Exception e)
        {
            await userManager.DeleteAsync(user);
            throw new AuthException("Could not add roles to the user",
                e);
        }

    }

    public Task<ApplicationUser?> GetUserAsync(string id)
    {
        return userManager.FindByIdAsync(id);
    }

    public Task<ApplicationUser?> GetUserByEmailAsync(string id)
    {
        return userManager.FindByEmailAsync(id);
    }

    public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
    {
        return userManager.IsInRoleAsync(user, roleName);
    }
}