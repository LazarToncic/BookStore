namespace BookStore.Application.Common.Dto.OrderItems;

public record GetSingleUserOrdersItemsDto(string BookName, int Quantity, int Price);