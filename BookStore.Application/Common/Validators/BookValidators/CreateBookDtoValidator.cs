using BookStore.Application.Common.Dto.Book;
using FluentValidation;

namespace BookStore.Application.Common.Validators.BookValidators;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Book name is required.");
        
        RuleFor(x => x.ReleaseYear)
            .GreaterThan(0)
            .LessThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("Release year must be a positive number.");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.");
        
        RuleFor(x => new[] { x.BookCategory1, x.BookCategory2, x.BookCategory3, x.BookCategory4 })
            .Must(HaveAtLeastOneValidCategory)
            .WithMessage("At least one book category must be provided.");
    }
    
    private bool HaveAtLeastOneValidCategory(string?[] categories)
    {
        return categories.Any(category => !string.IsNullOrWhiteSpace(category));
    }
}