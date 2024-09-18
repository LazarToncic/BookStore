namespace BookStore.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    
    public string UserId { get; set; }

    public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    public ApplicationUser User { get; set; }

    public Order()
    {
        Id = Guid.NewGuid();
        OrderDate = DateTime.Now;
    }

    public Order AddUser(ApplicationUser user)
    {
        User = user;
        return this;
    }
}