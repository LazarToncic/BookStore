using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Services;

public class LoyaltyProgramService(IDemoDbContext dbContext) : ILoyaltyProgramService
{
    public async Task<Order> CreatingOrderLoyaltyProgram(int totalPrice, List<KeyValuePair<int, int>> orderedItemsPrices, string userId)
    {
        var loyaltyProgram = await dbContext.LoyaltyPrograms
            .Where(x => x.UserId.Equals(userId))
            .FirstOrDefaultAsync();

        if (loyaltyProgram == null)
            throw new Exception("LoyaltyProgram cant be find and with that order coudnt be completed");

        var checkedLoyaltyProgram = CheckIfWithThisDiscountPercentageExists(loyaltyProgram, totalPrice);

        if (checkedLoyaltyProgram.DiscountPercentage == 100)
        {
            var maxPricePair = orderedItemsPrices.OrderByDescending(kvp => kvp.Key).FirstOrDefault();

            if (maxPricePair.Key > 0)
            {
                var updatedQuantity = maxPricePair.Value - 1;

                orderedItemsPrices.Remove(maxPricePair);

                if (updatedQuantity > 0)
                {
                    orderedItemsPrices.Add(new KeyValuePair<int, int>(maxPricePair.Key, updatedQuantity));
                }

                loyaltyProgram.LoyaltyPoints = 0;
                loyaltyProgram.DiscountPercentage = 0;

                dbContext.LoyaltyPrograms.Update(loyaltyProgram);
                await dbContext.SaveChangesAsync(new CancellationToken());

                var totalPriceAfterFreeBook = totalPrice - maxPricePair.Key;

                var totalPriceAfterTotalDiscountRepeated = (30 / 100.0) * totalPriceAfterFreeBook;

                var totalPriceMinusDiscount = totalPriceAfterFreeBook - totalPriceAfterTotalDiscountRepeated;

                return new Order(Convert.ToInt32(totalPriceMinusDiscount))
                    .AddDiscounts(true, 30, true);
            }

            throw new Exception("No book was selected");
        }

        if (checkedLoyaltyProgram.DiscountPercentage != 0)
        {
            var totalPriceAfterTotalDisocunt = (checkedLoyaltyProgram.DiscountPercentage / 100) * totalPrice;

            loyaltyProgram.LoyaltyPoints = checkedLoyaltyProgram.LoyaltyPoints;
            loyaltyProgram.DiscountPercentage = checkedLoyaltyProgram.DiscountPercentage;

            dbContext.LoyaltyPrograms.Update(loyaltyProgram);
            await dbContext.SaveChangesAsync(new CancellationToken());

            var totalPriceMinusDiscount = totalPrice - totalPriceAfterTotalDisocunt;

            return new Order(Convert.ToInt32(totalPriceMinusDiscount))
                .AddDiscounts(true, checkedLoyaltyProgram.DiscountPercentage, false);
        }

        loyaltyProgram.LoyaltyPoints = checkedLoyaltyProgram.LoyaltyPoints;
        loyaltyProgram.DiscountPercentage = checkedLoyaltyProgram.DiscountPercentage;

        dbContext.LoyaltyPrograms.Update(loyaltyProgram);
        await dbContext.SaveChangesAsync(new CancellationToken());

        return new Order(totalPrice)
            .AddDiscounts(false, null, false);
    }
    

    private LoyaltyProgram CheckIfWithThisDiscountPercentageExists(LoyaltyProgram loyaltyProgram, int totalPrice)
    {
        var points = CalculateLoyaltyPoints(loyaltyProgram.LoyaltyPoints, totalPrice);

        var newLoyaltyProgram = new LoyaltyProgram
        {
            LoyaltyPoints = points
        };

        if (points >= 0 && points < 10)
        {
            newLoyaltyProgram.DiscountPercentage = 0;
        }
        else if (points >= 10 && points < 20)
        {
            newLoyaltyProgram.DiscountPercentage = 10;
        } else if (points >= 20 && points < 30) 
        {
            newLoyaltyProgram.DiscountPercentage = 20;
        } else if (points >= 30 && points < 50)
        {
            newLoyaltyProgram.DiscountPercentage = 30;
        } else if (points >= 50)
        {
            newLoyaltyProgram.DiscountPercentage = 100;
        }

        return newLoyaltyProgram;
    }

    private int CalculateLoyaltyPoints(int currentLoyalPoint, int totalPrice)
    {
        var points = 0;
        
            if (totalPrice >= 500 && totalPrice < 1000)
            {
                points += 2;
            } else if (totalPrice >= 1000 && totalPrice < 3000)
            {
                points += 4;
            } else if (totalPrice >= 3000 && totalPrice < 5000)
            {
                points += 6;
            } else if (totalPrice >= 5000)
            {
                points += 8;
            }

        points += currentLoyalPoint;

        return points;
    }
}