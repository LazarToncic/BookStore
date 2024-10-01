using BookStore.Application.Common.Dto.Author;
using BookStore.Application.Common.Dto.Book;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.Book;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Book.Commands;

public record CreateBookCommand(CreateBookDto CreateBookDto, List<CreateAuthorDto> CreateAuthorsDto) : IRequest;

public class CreateBookCommandHandler(IDemoDbContext dbContext, IAuthorService authorService) : IRequestHandler<CreateBookCommand>
{
    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    { 
        var existingBook = await dbContext.Book.Where(x => x.Name.Equals(request.CreateBookDto.Name) &&
                                                   x.ReleaseYear.Equals(request.CreateBookDto.ReleaseYear))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (existingBook != null)
            throw new BaseException("This book already exists!");

        var book = request.CreateBookDto.FromDtoToEntity();
        
        dbContext.Book.Add(book);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        await AddCategoriesAsync(request.CreateBookDto, book.Id, cancellationToken);
        await CreateAuthorsAsync(request.CreateAuthorsDto, book.Id, cancellationToken);
    }

    private async Task AddCategoriesAsync(CreateBookDto dto, Guid bookId, CancellationToken cancellationToken)
    {
        var categories = new[] 
        { 
            dto.BookCategory1, 
            dto.BookCategory2, 
            dto.BookCategory3, 
            dto.BookCategory4 
        };

        var newCategories = new List<Domain.Entities.BookCategory>();
        var bookCategoryBooks = new List<BookCategoryBook>();
        
        foreach (var category in categories)
        {
            if (!string.IsNullOrWhiteSpace(category))
            {
                var existingCategory = await dbContext.BookCategory
                    .Where(x => x.BookCategoryName.Equals(category.ToLower()))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                        

                if (existingCategory == null)
                {
                    var newCategory = new Domain.Entities.BookCategory(category);
                    newCategories.Add(newCategory);
                    bookCategoryBooks.Add(new BookCategoryBook(bookId, newCategory.Id));
                }
                else
                {
                    bookCategoryBooks.Add(new BookCategoryBook(bookId, existingCategory.Id));
                }
                
            }
        }
        
        if (newCategories.Count > 0)
        {
            await dbContext.BookCategory.AddRangeAsync(newCategories, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        await dbContext.BookCategoryBook.AddRangeAsync(bookCategoryBooks, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task CreateAuthorsAsync(List<CreateAuthorDto> listOfAuthors, Guid bookId, CancellationToken cancellationToken)
    {
        var authorIds = await authorService.CreateAuthors(listOfAuthors, cancellationToken);

        foreach (var authorId in authorIds)
        {
            dbContext.AuthorsBooks.Add(new AuthorsBooks(authorId, bookId));
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 