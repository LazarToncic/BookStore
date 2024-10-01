using BookStore.Application.Common.Dto.Author;
using FluentValidation;

namespace BookStore.Application.Common.Validators.AuthorValidators;

public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("You need to enter full name of the author");

        RuleFor(x => x.YearOfBirth)
            .NotEmpty()
            .InclusiveBetween(1800, DateTime.Now.Year).WithMessage("Year of birth must be between 1800 and the current year.");
    }
}