using BookStore.Application.Common.Dto.Order;
using BookStore.Application.Common.Dto.OrderItems;
using BookStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Order.Queries;

public record GetOrdersForSingleUserQuery() : IRequest<GetOrdersForSingleUserQueryResponseDto>;

public class GetOrdersForSingleUserQueryHandler(IDemoDbContext dbContext, ICurrentUserService currentUserService) : IRequestHandler<GetOrdersForSingleUserQuery, GetOrdersForSingleUserQueryResponseDto>
{
    public async Task<GetOrdersForSingleUserQueryResponseDto> Handle(GetOrdersForSingleUserQuery request, CancellationToken cancellationToken)
    {
        var loggedInUserId = currentUserService.GetCurrentUser();

        var orders = await dbContext.Order
            .Where(x => x.UserId == loggedInUserId)
            .Include(x => x.OrderItems)
            .ToListAsync(cancellationToken: cancellationToken);
        
        var orderDtos = orders.Select(o => new OrderDto(
            OrderDate: o.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"),
            TotalPrice: o.TotalPrice,
            DiscountActive: o.DiscountActive,
            OneFreeBookDiscount: o.OneFreeBookDiscount,
            DiscountAmount: o.DiscountAmount,
            OrderItems: o.OrderItems.Select(oi => new GetSingleUserOrdersItemsDto(
                BookName: oi.BookName,
                Quantity: oi.Quantity,
                Price: oi.Price
            )).ToList()
        )).ToList();

        return new GetOrdersForSingleUserQueryResponseDto(orderDtos);
    }
} 