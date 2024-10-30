using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.LoyaltyProgram.Commands;

public record SetLoyaltyProgramToDefaultForAllUsersCommand() : IRequest;

public class SetLoyaltyProgramToDefaultForAllUsersCommandHandler(IDemoDbContext demoDbContext) : IRequestHandler<SetLoyaltyProgramToDefaultForAllUsersCommand>
{
    public async Task Handle(SetLoyaltyProgramToDefaultForAllUsersCommand request, CancellationToken cancellationToken)
    {
        var usersWithLoyaltyPrograms = await demoDbContext.Users
            .Include(x => x.LoyaltyProgram)
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var user in usersWithLoyaltyPrograms)
        {
            user.LoyaltyProgram.LoyaltyPoints = 0;
            user.LoyaltyProgram.DiscountPercentage = 0;
        }

        await demoDbContext.SaveChangesAsync(cancellationToken);
    }
} 