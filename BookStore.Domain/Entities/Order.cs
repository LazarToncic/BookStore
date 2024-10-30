namespace BookStore.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    
    public string UserId { get; set; }

    public int TotalPrice { get; set; }
    public bool DiscountActive { get; set; }
    public bool OneFreeBookDiscount { get; set; }
    public decimal? DiscountAmount { get; set; }

    public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    public ApplicationUser User { get; set; }

    public Order(int totalPrice)
    {
        Id = Guid.NewGuid();
        OrderDate = DateTime.UtcNow;
        TotalPrice = totalPrice;
    }

    public Order AddUser(ApplicationUser user)
    {
        User = user;
        return this;
    }

    public Order AddDiscounts(bool discountActive, decimal? discountAmount, bool oneFreeBookDiscount)
    {
        DiscountActive = discountActive;
        
        if (discountAmount > 0) 
            DiscountAmount = discountAmount;

        OneFreeBookDiscount = oneFreeBookDiscount;

        return this;
    }
}