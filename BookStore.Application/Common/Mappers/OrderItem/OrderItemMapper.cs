using BookStore.Application.Common.Dto.OrderItems;
using Riok.Mapperly.Abstractions;

namespace BookStore.Application.Common.Mappers.OrderItem;

[Mapper]
public static partial class OrderItemMapper
{
    public static Domain.Entities.OrderItem FromOrderItemDtoToOrderItem(this OrderItemDto dto, Guid orderId)
    {
        return new Domain.Entities.OrderItem(orderId, dto.Quantity, dto.BookId, dto.BookName);
    }
}