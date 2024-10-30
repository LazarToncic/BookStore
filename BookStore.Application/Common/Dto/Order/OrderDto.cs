using BookStore.Application.Common.Dto.OrderItems;

namespace BookStore.Application.Common.Dto.Order;

public record OrderDto(string OrderDate, int TotalPrice, bool DiscountActive, bool OneFreeBookDiscount, decimal? DiscountAmount, 
    IList<GetSingleUserOrdersItemsDto> OrderItems);