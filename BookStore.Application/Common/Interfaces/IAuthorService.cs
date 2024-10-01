using BookStore.Application.Common.Dto.Author;

namespace BookStore.Application.Common.Interfaces;

public interface IAuthorService
{
    public Task CreateAuthor(CreateAuthorDto dto, CancellationToken cancellationToken);
    public Task<List<Guid>> CreateAuthors(List<CreateAuthorDto> authorDtos, CancellationToken cancellationToken);
}