using BookStore.Application.Common.Dto.Book;
using BookStore.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace BookStore.Application.Common.Mappers.Book;

[Mapper]
public static partial class BookMapper
{
    public static partial Domain.Entities.Book FromDtoToEntity(this CreateBookDto createBookDto);
    public static partial BookWithItsCategories FromEntityToBookWithItsCategories(this Domain.Entities.Book book);

    public static GetBookDetailsResponseDto FromEntityToGetBookDetailsResponseDto(this Domain.Entities.Book book, List<string> categories)
    {
        return new GetBookDetailsResponseDto(book.Name, book.ReleaseYear, book.Description, categories);
    }
    
    public static GetAllBooksQueryResponseDto FromEntityToGetAllBooksQueryResponseDto(this List<BookWithItsCategories> books, int pageNumber, int pageSize, int totalCount)
    {
        return new GetAllBooksQueryResponseDto(books, totalCount, pageNumber, pageSize);
    }
}