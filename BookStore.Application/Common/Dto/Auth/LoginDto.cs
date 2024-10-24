namespace BookStore.Application.Common.Dto.Auth;

public record LoginDto(string EmailOrUsername, string Password, bool RememberMe);