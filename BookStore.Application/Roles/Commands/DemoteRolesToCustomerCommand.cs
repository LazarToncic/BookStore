using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Roles.Commands;

public record DemoteRolesToCustomerCommand(string UserId) : IRequest;

public class DemoteRolesToCustomerCommandHandler(IRoleService roleService, IDemoDbContext demoDbContext)
    : IRequestHandler<DemoteRolesToCustomerCommand>
{
    public async Task Handle(DemoteRolesToCustomerCommand request, CancellationToken cancellationToken)
    {
        var loggedUserRole = await roleService.GetStrongestRoleForCurrentUser();

        var roleNames = await demoDbContext.Users
            .Where(x => x.Id == request.UserId)
            .Include(x => x.Roles)
            .ThenInclude(ur => ur.Role)
            .SelectMany(u => u.Roles.Select(ur => ur.Role.Name))
            .Where(roleName => roleName != null)
            .Select(roleName => roleName!)
            .ToListAsync(cancellationToken);

        var demotingUserStrongestRole = roleService.GetStrongestRoleForUser(roleNames);

        if (CheckIfUserCanDoDemotion(loggedUserRole, demotingUserStrongestRole))
        {
            var userToDemote = await demoDbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (userToDemote != null)
            {
                
                var customerRole = await demoDbContext.Roles
                    .FirstOrDefaultAsync(r => r.Name == "Customer", cancellationToken);

                if (customerRole == null)
                {
                    throw new Exception("Customer role not found.");
                }
                
                userToDemote.Roles.Clear();
                userToDemote.Roles.Add(new ApplicationUserRole { RoleId = customerRole.Id, UserId = userToDemote.Id });
                
                await demoDbContext.SaveChangesAsync(cancellationToken);
            }
        }else
        {
            throw new UnauthorizedAccessException("You do not have permission to demote this user.");
        }
    }

    private bool CheckIfUserCanDoDemotion(string loggedUserRole, string demotingUserRole)
    {
        if (loggedUserRole == "Owner")
        {
            return true;
        }

        if (loggedUserRole == "StoreManager" && demotingUserRole == "Employee")
        {
            return true;
        }

        if (loggedUserRole == "StoreManager" && demotingUserRole == "StoreManager")
        {
            return false;
        }

        return false;
    }
    
}