using BookStore.Application.Common.Validators.AuthValidators.LoginValidators;
using FluentValidation;

namespace BookStore.Application.Auth.Commands.LoginCommand;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.LoginDto)
            .SetValidator(new LoginDtoValidator());
    }
}