using FluentValidation;

namespace BookStore.Application.Auth.Commands.BeginLoginCommand;

public class BeginLoginModelValidator : AbstractValidator<BeginLoginCommand>
{
    public BeginLoginModelValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty();
    }
}