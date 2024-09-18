using BookStore.Application.Common.Dto.Book;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.Book;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Book.Commands;

public record CreateBookCommand(CreateBookDto CreateBookDto) : IRequest;

public class CreateBookCommandHandler(IDemoDbContext dbContext) : IRequestHandler<CreateBookCommand>
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

        var bookId = book.Id;

        var categories = new[] 
        { 
            request.CreateBookDto.BookCategory1, 
            request.CreateBookDto.BookCategory2, 
            request.CreateBookDto.BookCategory3, 
            request.CreateBookDto.BookCategory4 
        };

        foreach (var category in categories)
        {
            if (!string.IsNullOrWhiteSpace(category))
            {
                var existingCategory = await dbContext.BookCategory
                    .Where(x => x.BookCategoryName.Equals(category.ToLower()))
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                        

                if (existingCategory == null)
                {
                    var newBook = new Domain.Entities.BookCategory(category);
                    dbContext.BookCategory.Add(newBook);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    
                    dbContext.BookCategoryBook.Add(new BookCategoryBook(bookId, newBook.Id));
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    dbContext.BookCategoryBook.Add(new BookCategoryBook(bookId, existingCategory.Id));
                    await dbContext.SaveChangesAsync(cancellationToken);    
                }
                
            }
        }
        
    }
} 