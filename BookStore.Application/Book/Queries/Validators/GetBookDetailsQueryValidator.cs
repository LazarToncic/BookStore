using FluentValidation;

namespace BookStore.Application.Book.Queries.Validators;

public class GetBookDetailsQueryValidator : AbstractValidator<GetBookDetailsQuery>
{
    public GetBookDetailsQueryValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("Book ID cannot be empty.")
            .Must(BeAValidGuid).WithMessage("Book ID must be a valid GUID.");
    }
    
    private bool BeAValidGuid(string bookId)
    {
        return Guid.TryParse(bookId, out _);
    }
}