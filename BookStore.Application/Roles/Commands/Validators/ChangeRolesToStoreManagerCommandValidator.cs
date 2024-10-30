using FluentValidation;

namespace BookStore.Application.Roles.Commands.Validators;

public class ChangeRolesToStoreManagerCommandValidator : AbstractValidator<ChangeRolesToStoreManagerCommand>
{
    public ChangeRolesToStoreManagerCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .Must(BeAValidGuid).WithMessage("UserId must be a valid GUID.");
    }

    private bool BeAValidGuid(string userId)
    {
        return Guid.TryParse(userId, out _);
    }
}