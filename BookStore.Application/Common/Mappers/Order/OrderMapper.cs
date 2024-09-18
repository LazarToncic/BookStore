using BookStore.Application.Common.Dto.Order;
using Riok.Mapperly.Abstractions;

namespace BookStore.Application.Common.Mappers.Order;

[Mapper]
public static partial class OrderMapper
{
    public static partial Domain.Entities.Order FromCreateOrderCommandDtoToOrder(this CreateOrderCommandDto createOrderCommandDto);
}