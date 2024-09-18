using BookStore.Application.Book.Queries;
using FluentValidation;

namespace BookStore.Application.Common.Dto.Book;

public class GetBookDetailsQueryValidator : AbstractValidator<GetBookDetailsQuery>
{
    public GetBookDetailsQueryValidator()
    {
        RuleFor(x => x.BookId).NotEmpty().WithMessage("Id ne moze da bude prazan");
    }
}