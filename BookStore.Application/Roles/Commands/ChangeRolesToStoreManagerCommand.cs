using BookStore.Application.Common.Dto.Role;
using BookStore.Application.Common.Interfaces;
using MediatR;

namespace BookStore.Application.Roles.Commands;

public record ChangeRolesToStoreManagerCommand(string UserId) : IRequest<ChangeRolesResponseDto>;

public class ChangeRolesToStoreManagerCommandHandler(IRoleService roleService, ICurrentUserService currentUserService) 
    : IRequestHandler<ChangeRolesToStoreManagerCommand, ChangeRolesResponseDto>
{
    public async Task<ChangeRolesResponseDto> Handle(ChangeRolesToStoreManagerCommand request, CancellationToken cancellationToken)
    {
        var requestingUserId = currentUserService.GetCurrentUser();
        
        return await roleService.ChangeRolesForUser(request.UserId, requestingUserId, "StoreManager");
    }
} 