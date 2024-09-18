using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Infrastructure.Identity;

public class RoleServices(RoleManager<ApplicationRole> roleManager) : IRoleService
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
}