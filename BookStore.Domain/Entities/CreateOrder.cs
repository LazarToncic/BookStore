namespace BookStore.Domain.Entities;

public class CreateOrder
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string UserId { get; set; }

    public CreateOrder()
    {
        Id = Guid.NewGuid();
        OrderDate = DateTime.Now;
    }

    public CreateOrder AddUserId(string userId)
    {
        UserId = userId;
        return this;
    }
}