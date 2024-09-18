using BookStore.Application.Common.Validators.BookValidators;
using FluentValidation;

namespace BookStore.Application.Book.Commands;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.CreateBookDto)
            .SetValidator(new CreateBookDtoValidator());
    }
}