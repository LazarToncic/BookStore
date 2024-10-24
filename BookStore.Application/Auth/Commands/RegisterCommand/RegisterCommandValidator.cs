using BookStore.Application.Common.Validators.AuthValidators.RegisterValidators;
using FluentValidation;

namespace BookStore.Application.Auth.Commands.RegisterCommand;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.RegisterDto)
            .SetValidator(new RegisterDtoValidator());
    }
}