using BookStore.Application.Common.Dto.Role;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Infrastructure.Identity;

public class RoleServices(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) : IRoleService
{
    public async Task CreateRoleAsync(string role)
    {
        var alreadyExists = await roleManager.RoleExistsAsync(role);

        if (!alreadyExists)
        {
            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = role,
                NormalizedName = role.Normalize()
            });
        }
    }

    public async Task<ChangeRolesResponseDto> ChangeRolesForUser(string userId, string requestingUserId, string promotedRole)
    {
        var user = await userManager.FindByIdAsync(userId);
        var requestingUser = await userManager.FindByIdAsync(requestingUserId);

        if (user == null || requestingUser == null) return new ChangeRolesResponseDto(false, "User doesnt exist");

        var requestingUserRoles = await userManager.GetRolesAsync(requestingUser);
        
        switch (promotedRole)
        {
            case "Employee":
            {
                if (requestingUserRoles.Contains("StoreManager") || requestingUserRoles.Contains("Owner"))
                {
                    return await ChangeRoles(user, promotedRole);
                }
                
                return new ChangeRolesResponseDto(false, "You cant add Employee Role");
            }
            case "StoreManager":
            {
                if (requestingUserRoles.Contains("Owner"))
                {
                    return await ChangeRoles(user, promotedRole);
                }
            
                return new ChangeRolesResponseDto(false, "You cant add StoreManager Role");
            }
            default:
                return new ChangeRolesResponseDto(false, "There was an error changing roles");
        }
    }

    private async Task<ChangeRolesResponseDto> ChangeRoles(ApplicationUser user, string roleName)
    {
        var currentRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentRoles);
        var result = await userManager.AddToRoleAsync(user, roleName);

        return new ChangeRolesResponseDto(result.Succeeded, "Changes are complete");;
    }
}