using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookCategory.Commands;

public record CreateBookCategoryCommand(string BookCategoryName) : IRequest;

public class CreateBookCategoryCommandHandler(IDemoDbContext dbContext) : IRequestHandler<CreateBookCategoryCommand>
{
    public async Task Handle(CreateBookCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await dbContext.BookCategory
            .Where(x => x.BookCategoryName == request.BookCategoryName)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (category != null)
        {
            throw new BaseException("Ova kategorija vec postoji");
        }

        dbContext.BookCategory.Add(new Domain.Entities.BookCategory(request.BookCategoryName.ToLower()));
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 