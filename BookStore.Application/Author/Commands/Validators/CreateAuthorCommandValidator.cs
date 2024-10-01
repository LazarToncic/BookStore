using BookStore.Application.Common.Validators.AuthorValidators;
using FluentValidation;

namespace BookStore.Application.Author.Commands.Validators;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.Dto)
            .SetValidator(new CreateAuthorDtoValidator());
    }
}