using BookStore.Application.Common.Validators.AuthorValidators;
using BookStore.Application.Common.Validators.BookValidators;
using FluentValidation;

namespace BookStore.Application.Book.Commands;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.CreateBookDto)
            .SetValidator(new CreateBookDtoValidator());
        
        RuleFor(x => x.CreateAuthorsDto)
            .Must(authors => authors.Count >= 1 && authors.Count <= 4)
            .WithMessage("The number of authors must be between 1 and 4.");

        RuleForEach(x => x.CreateAuthorsDto)
            .SetValidator(new CreateAuthorDtoValidator());
    }
}