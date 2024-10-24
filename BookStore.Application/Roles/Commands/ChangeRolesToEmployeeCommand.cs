using System.Security.Authentication;
using System.Security.Claims;
using BookStore.Application.Common.Dto.Role;
using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.Roles.Commands;

public record ChangeRolesToEmployeeCommand(string UserId) : IRequest<ChangeRolesResponseDto>;

public class ChangeRolesCommandHandler(IRoleService roleService, ICurrentUserService currentUserService) : IRequestHandler<ChangeRolesToEmployeeCommand, ChangeRolesResponseDto>
{
    public async Task<ChangeRolesResponseDto> Handle(ChangeRolesToEmployeeCommand request, CancellationToken cancellationToken)
    {
        var requestingUserId = currentUserService.GetCurrentUser();
        
        return await roleService.ChangeRolesForUser(request.UserId, requestingUserId, "Employee");
    }
} 