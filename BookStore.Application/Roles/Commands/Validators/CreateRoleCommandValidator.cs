using FluentValidation;

namespace BookStore.Application.Roles.Commands.Validators;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role name cannot be empty.")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.")
            .MaximumLength(16).WithMessage("Role name must not exceed 16 characters.")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Role name can only contain alphanumeric characters and spaces.");
    }
}