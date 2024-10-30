namespace BookStore.Domain.Entities;

public class LoyaltyProgram
{
    public Guid Id { get; set; }
    public int LoyaltyPoints { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime EnrollmentDate { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public LoyaltyProgram()
    {
        Id = Guid.NewGuid();
    }
}