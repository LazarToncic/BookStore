using BookStore.Application.Common.Dto.OrderItems;
using Riok.Mapperly.Abstractions;

namespace BookStore.Application.Common.Mappers.OrderItem;

[Mapper]
public static partial class OrderItemMapper
{
    public static Domain.Entities.OrderItem FromOrderItemDtoToOrderItem(this OrderItemDto dto, Guid orderId, int bookPrice)
    {
        return new Domain.Entities.OrderItem(orderId, dto.Quantity, dto.Quantity * bookPrice, dto.BookId, dto.BookName);
    }
}