using BookStore.Application.BookCategory.Commands;
using FluentValidation;

namespace BookStore.Application.Common.Validators.BookCategoryValidators;

public class CreateBookCategoryCommandValidator : AbstractValidator<CreateBookCategoryCommand>
{
    public CreateBookCategoryCommandValidator()
    {
        RuleFor(x => x.BookCategoryName)
            .MinimumLength(3)
            .MaximumLength(20)
            .NotEmpty();
    }
}