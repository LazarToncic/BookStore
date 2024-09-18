namespace BookStore.Application.Common.Dto.Book;

public record CreateBookDto(string Name, int ReleaseYear, string Description, string BookCategory1, string BookCategory2, string BookCategory3, string BookCategory4);