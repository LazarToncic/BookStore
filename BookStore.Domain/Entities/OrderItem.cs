namespace BookStore.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid OrderId { get; set; }
    public string BookName { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public Order Order { get; set; }

    public OrderItem(Guid orderId, int quantity, int price, Guid bookId, string bookName)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        BookId = bookId;
        BookName = bookName;
        Quantity = quantity;
        Price = price;
    }
}