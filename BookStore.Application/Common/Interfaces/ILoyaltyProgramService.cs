using BookStore.Application.Common.Dto.OrderItems;

namespace BookStore.Application.Common.Interfaces;

public interface ILoyaltyProgramService
{
    Task<Domain.Entities.Order> CreatingOrderLoyaltyProgram(int totalPrice, List<KeyValuePair<int, int>> orderedItemsPrices, string userId);
}