using BookStore.Application.Common.Dto.Book;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Extensions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.Book;
using BookStore.Application.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookStore.Application.Book.Queries;

public record GetBookDetailsQuery(string BookId) : IRequest<GetBookDetailsResponseDto>;

public class GetBookDetailsQueryHandler(IDemoDbContext dbContext) : IRequestHandler<GetBookDetailsQuery, GetBookDetailsResponseDto>
{
    public async Task<GetBookDetailsResponseDto> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
    {
        
        
        var bookWithCategories = await dbContext.Book
            .Include(bc => bc.BookCategoryBooks)
            .ThenInclude(c => c.BookCategory)
            .Where(x => x.Id.Equals(Guid.Parse(request.BookId)))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (bookWithCategories == null)
            throw new NotFoundException("This book doesnt exist");

        var categories = bookWithCategories.BookCategoryBooks.Select(bc => bc.BookCategory.BookCategoryName).ToList();

        return bookWithCategories.FromEntityToGetBookDetailsResponseDto(categories);
    }
} 