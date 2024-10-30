using BookStore.Application.Common.Dto.Users;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.User;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Users.Queries;

public record GetAllUsersQuery(GetAllUsersQueryDto? SearchCriteria) : IRequest<GetAllUsersQueryResponseDto>;

public class GetAllUsersQueryHandler(IDemoDbContext dbContext, IRoleService roleService)
    : IRequestHandler<GetAllUsersQuery, GetAllUsersQueryResponseDto>
{
    public async Task<GetAllUsersQueryResponseDto> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Users
            .Include(u => u.Roles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.LoyaltyProgram)
            .AsQueryable();

        var currentUserStrongestRole = await roleService.GetStrongestRoleForCurrentUser();

        query = GetUsersByTheirRoles(currentUserStrongestRole, query);

        if (request.SearchCriteria == null)
        {
            var users = await query.ToListAsync(cancellationToken);

            return users.FromListApplicationUserToGetAllUsersQueryResponseDto();
        }

        if (!string.IsNullOrWhiteSpace(request.SearchCriteria.FirstName))
        {
            query = query.Where(u => u.FirstName.Contains(request.SearchCriteria.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchCriteria.LastName))
        {
            query = query.Where(u => u.LastName.Contains(request.SearchCriteria.LastName));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchCriteria.Username))
        {
            query = query.Where(u => u.UserName.Contains(request.SearchCriteria.Username));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchCriteria.Email))
        {
            query = query.Where(u => u.Email.Contains(request.SearchCriteria.Email));
        }

        var filteredUsers = await query.ToListAsync(cancellationToken);

        return filteredUsers.FromListApplicationUserToGetAllUsersQueryResponseDto();
    }

    private IQueryable<ApplicationUser> GetUsersByTheirRoles(string strongestRole, IQueryable<ApplicationUser> query)
    {
        if (strongestRole == "Owner")
        {
            query = query.Include(u => u.Roles).ThenInclude(ur => ur.Role);
        }
        else if (strongestRole == "StoreManager")
        {
            query = query.Include(u => u.Roles).ThenInclude(ur => ur.Role)
                .Where(u => u.Roles.Any(r => r.Role.Name == "Customer" || r.Role.Name == "Employee"));
        }
        else if (strongestRole == "Employee")
        {
            query = query.Include(u => u.Roles).ThenInclude(ur => ur.Role)
                .Where(u => u.Roles.Any(r => r.Role.Name == "Customer"));
        }

        return query;
    }
}