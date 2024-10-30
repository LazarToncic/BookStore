using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.LoyaltyProgram.Commands;

public record SetLoyaltyProgramToDefaultForSingleUserCommand(string UserId) : IRequest;

public class SetLoyaltyProgramToDefaultForSingleUserCommandHandler(IDemoDbContext demoDbContext) : IRequestHandler<SetLoyaltyProgramToDefaultForSingleUserCommand>
{
    public async Task Handle(SetLoyaltyProgramToDefaultForSingleUserCommand request, CancellationToken cancellationToken)
    {
        var user = await demoDbContext.Users
            .Include(x => x.LoyaltyProgram)
            .Where(x => x.Id.Equals(request.UserId))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (user == null)
            throw new Exception("This user doesnt exist");

        user.LoyaltyProgram.LoyaltyPoints = 0;
        user.LoyaltyProgram.DiscountPercentage = 0;

        await demoDbContext.SaveChangesAsync(cancellationToken);
    }
}  