namespace BookStore.Application.Common.Dto.OrderItems;

public record OrderItemDto(Guid BookId, string BookName, int Quantity);