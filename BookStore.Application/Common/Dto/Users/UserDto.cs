namespace BookStore.Application.Common.Dto.Users;

public record UserDto(string FirstName, string LastName, string Username, string Email, string RoleName, int Loyaltypoints, 
    decimal DiscountPercentage, DateTime EnrollmentDate);