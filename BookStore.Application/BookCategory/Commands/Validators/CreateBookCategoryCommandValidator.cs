using FluentValidation;

namespace BookStore.Application.BookCategory.Commands.Validators;

public class CreateBookCategoryCommandValidator : AbstractValidator<CreateBookCategoryCommand>
{
    public CreateBookCategoryCommandValidator()
    {
        RuleFor(x => x.BookCategoryName)
            .NotEmpty().WithMessage("You need to enter Category Name")
            .MinimumLength(5).WithMessage("Category name must be at least 5 letters long")
            .MaximumLength(30).WithMessage("Category name must be below 30 letters long")
            .Matches("^[a-zA-Z]+$").WithMessage("Category name must contain only letters.");
    }
}