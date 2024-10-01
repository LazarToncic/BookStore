using BookStore.Application.Common.Dto.Order;
using BookStore.Application.Common.Dto.OrderItems;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.OrderItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Order.Commands;

public record CreateOrderCommand(CreateOrderCommandDto OrderDto) : IRequest; // validator se ne registruje

public class CreateOrderCommandHandler(IDemoDbContext dbContext) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        //ne bi trebao da se salje userId nego da se vidi dal si ulogovan i onda samo da se izvuce iz CurrentUserService

        var user = await dbContext.Users
            .Where(x => x.Id.Equals(request.OrderDto.UserId.ToString()))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (user == null)
            throw new NotFoundException("This user doesnt exist");

        var totalPrice = 0;
        
        foreach (var item in request.OrderDto.Items)
        {
            var singleBookPrice = await GetSingleBookPrice(item, cancellationToken);

            totalPrice += singleBookPrice;
        }
        
        var order = new Domain.Entities.Order(totalPrice).AddUser(user);
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