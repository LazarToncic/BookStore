namespace BookStore.Application.Common.Interfaces;

public interface IRoleService
{
    Task CreateRoleAsync(string role);
}