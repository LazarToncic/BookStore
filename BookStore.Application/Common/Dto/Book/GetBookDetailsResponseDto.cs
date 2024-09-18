namespace BookStore.Application.Common.Dto.Book;

public record GetBookDetailsResponseDto(string Name, int ReleaseYear, string Description, List<string> Categories);