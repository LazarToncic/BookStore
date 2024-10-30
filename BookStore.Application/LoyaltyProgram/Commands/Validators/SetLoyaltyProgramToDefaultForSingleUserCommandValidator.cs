using FluentValidation;

namespace BookStore.Application.LoyaltyProgram.Commands.Validators;

public class SetLoyaltyProgramToDefaultForSingleUserCommandValidator : AbstractValidator<SetLoyaltyProgramToDefaultForSingleUserCommand>
{
    public SetLoyaltyProgramToDefaultForSingleUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty()
            .WithMessage("UserId cant be empty");
    }
}