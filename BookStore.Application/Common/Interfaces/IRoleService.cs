using BookStore.Application.Common.Dto.Role;

namespace BookStore.Application.Common.Interfaces;

public interface IRoleService
{
    Task CreateRoleAsync(string role);
    Task<ChangeRolesResponseDto> ChangeRolesForUser(string userId, string requestingUserId, string promotedRole);
    Task<List<string>> GetCurrentUserRole();
    Task<string> GetStrongestRoleForCurrentUser();
    string GetStrongestRoleForUser(List<string> roles);
}