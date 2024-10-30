namespace BookStore.Application.Common.Dto.Users;

public record GetAllUsersQueryDto(string FirstName = "", string LastName = "", string Email = "", string Username = "");