namespace BookStore.Application.Common.Dto.Order;

public record GetOrdersForSingleUserQueryResponseDto(IList<OrderDto> Orders);