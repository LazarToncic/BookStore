using FluentValidation;

namespace BookStore.Application.Auth.Commands.CompleteLoginCommand;

public class CompleteLoginModelValidator : AbstractValidator<BookStore.Application.Auth.Commands.CompleteLoginCommand.CompleteLoginCommand>
{
    public CompleteLoginModelValidator()
    {
        RuleFor(x => x.ValidationToken)
            .NotEmpty();
    }
}