using BookStore.Application.Common.Dto.OrderItems;
using BookStore.Domain.Entities;

namespace BookStore.Application.Common.Dto.Order;

public record CreateOrderCommandDto(Guid UserId, List<OrderItemDto> Items);