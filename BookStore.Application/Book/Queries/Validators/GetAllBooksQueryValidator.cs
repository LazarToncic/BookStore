using BookStore.Application.Common.Dto.Pagination;
using FluentValidation;

namespace BookStore.Application.Book.Queries.Validators;

public class GetAllBooksQueryValidator : AbstractValidator<GetAllBooksQuery>
{
    public GetAllBooksQueryValidator()
    {
        RuleFor(x => x.PaginationDto)
            .SetValidator(new PaginationDtoValidator());
    }
}