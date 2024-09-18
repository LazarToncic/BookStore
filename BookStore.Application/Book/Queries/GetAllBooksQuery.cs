using BookStore.Application.Common.Dto.Book;
using BookStore.Application.Common.Dto.Pagination;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.Book;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Book.Queries;

public record GetAllBooksQuery(PaginationDto PaginationDto) : IRequest<GetAllBooksQueryResponseDto>;

public class GetAllBooksQueryHandler(IDemoDbContext dbContext) : IRequestHandler<GetAllBooksQuery, GetAllBooksQueryResponseDto>
{
    public async Task<GetAllBooksQueryResponseDto> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var bookWithCategoriesAndPagination = await dbContext.Book
            .Include(bc => bc.BookCategoryBooks)
            .ThenInclude(c => c.BookCategory)
            .Skip((request.PaginationDto.PageNumber - 1) * request.PaginationDto.PageSize)
            .Take(request.PaginationDto.PageSize)
            .ToListAsync(cancellationToken);
        
        if (bookWithCategoriesAndPagination == null)
            throw new NotFoundException("This book doesnt exist");

        // probao sam da stavim novo polje CategoriesForBook u obican Entity Book ali onda ovaj gore upit ne radi????
        var allBooks = new List<BookWithItsCategories>();
        
        foreach (var singleBook in bookWithCategoriesAndPagination)
        {
            var bookWithCategories = singleBook.FromEntityToBookWithItsCategories();
            
            var categoriesForSingleBook = singleBook.BookCategoryBooks
                .Select(bc => bc.BookCategory.BookCategoryName)
                .ToList();

            bookWithCategories.AddCategoriesForBook(categoriesForSingleBook);
            
            allBooks.Add(bookWithCategories);
        }

        return allBooks.FromEntityToGetAllBooksQueryResponseDto(request.PaginationDto.PageNumber, request.PaginationDto.PageSize, allBooks.Count);
    }
} 