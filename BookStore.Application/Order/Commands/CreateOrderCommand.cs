using BookStore.Application.Common.Dto.Order;
using BookStore.Application.Common.Dto.OrderItems;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.OrderItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Order.Commands;

public record CreateOrderCommand(CreateOrderCommandDto OrderDto) : IRequest;

public class CreateOrderCommandHandler(IDemoDbContext dbContext, ICurrentUserService currentUserService, ILoyaltyProgramService loyaltyProgramService) 
    : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var requestingUserId = currentUserService.GetCurrentUser();

        var user = await dbContext.Users
            .Where(x => x.Id.Equals(requestingUserId))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (user == null)
            throw new NotFoundException("This user doesnt exist");

        var totalPrice = 0;
        var bookPrices = new List<KeyValuePair<int, int>>();
        
        foreach (var item in request.OrderDto.Items)
        {
            var singleBookPrice = await GetSingleBookPrice(item, cancellationToken);

            totalPrice += singleBookPrice * item.Quantity;
            bookPrices.Add(new KeyValuePair<int, int>(singleBookPrice, item.Quantity));
        }

        var totalPriceWithLoyaltyProgramApplied = await loyaltyProgramService.CreatingOrderLoyaltyProgram(totalPrice, bookPrices, user.Id);
        
        var order = totalPriceWithLoyaltyProgramApplied.AddUser(user);
        
        dbContext.Order.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        var orderId = order.Id;

        foreach (var item in request.OrderDto.Items)
        {
            var singleBookPrice = await GetSingleBookPrice(item, cancellationToken);
            
            dbContext.OrderItem.Add(item.FromOrderItemDtoToOrderItem(orderId, singleBookPrice));
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<int> GetSingleBookPrice(OrderItemDto item, CancellationToken cancellationToken)
    {
        return await dbContext.Book
            .Where(x => x.Id.Equals(item.BookId))
            .Select(x => x.Price)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);;
    }
} 