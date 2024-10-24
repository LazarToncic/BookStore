namespace BookStore.Application.Common.Dto.Auth;

public record RegisterDto(string FirstName, string LastName, string Username, string Email, string Password, string PhoneNumber);