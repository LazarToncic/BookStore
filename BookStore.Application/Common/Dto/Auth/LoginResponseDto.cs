namespace BookStore.Application.Common.Dto.Auth;

public record LoginResponseDto(bool LoginSuccess, bool? IsLockedOut);