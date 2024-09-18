using BookStore.Domain.Entities;

namespace BookStore.Application.Common.Dto.Book;

public record GetAllBooksQueryResponseDto(List<BookWithItsCategories> AllBooks, int TotalCountOfBooks, int PageNumber, int PageSize);