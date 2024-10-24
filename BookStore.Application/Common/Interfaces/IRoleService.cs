using BookStore.Application.Common.Dto.Role;

namespace BookStore.Application.Common.Interfaces;

public interface IRoleService
{
    Task CreateRoleAsync(string role);
    Task<ChangeRolesResponseDto> ChangeRolesForUser(string userId, string requestingUserId, string promotedRole);
}