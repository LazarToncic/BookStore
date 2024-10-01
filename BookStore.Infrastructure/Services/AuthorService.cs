using BookStore.Application.Common.Dto.Author;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.Author;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Services;

public class AuthorService(IDemoDbContext dbContext) : IAuthorService
{
    public async Task CreateAuthor(CreateAuthorDto dto, CancellationToken cancellationToken)
    {
        var authorExists = await GetExistingAuthor(dto, cancellationToken);
        
        if (authorExists != null)
            throw new BaseException("This Author already exists.");

        dbContext.Authors.Add(dto.FromCreateAuthorDtoToAuthor());
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Guid>> CreateAuthors(List<CreateAuthorDto> authorDtos, CancellationToken cancellationToken)
    {
        var authors = new List<Guid>();
        var newAuthors = new List<Author>();

        foreach (var author in authorDtos)
        {
            var existingAuthor = await GetExistingAuthor(author, cancellationToken);

            if (existingAuthor != null)
            {
                authors.Add(existingAuthor.Id);
            }
            else
            {
                var newAuthor = author.FromCreateAuthorDtoToAuthor();
                newAuthors.Add(newAuthor);
            }
        }
        
        if (newAuthors.Count > 0)
        {
            dbContext.Authors.AddRange(newAuthors);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            authors.AddRange(newAuthors.Select(a => a.Id));
        }

        return authors;
    }

    private async Task<Author?> GetExistingAuthor(CreateAuthorDto dto, CancellationToken cancellationToken)
    {
        return await dbContext.Authors
            .Where(x => x.FullName.Equals(dto.FullName) && x.YearOfBirth.Equals(dto.YearOfBirth))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}