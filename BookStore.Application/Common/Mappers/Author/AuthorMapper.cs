using BookStore.Application.Common.Dto.Author;
using Riok.Mapperly.Abstractions;

namespace BookStore.Application.Common.Mappers.Author;

[Mapper]
public static partial class AuthorMapper
{
    public static partial Domain.Entities.Author FromCreateAuthorDtoToAuthor(this CreateAuthorDto dto);
}