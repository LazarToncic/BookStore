using BookStore.Application.Common.Dto.Auth;

namespace BookStore.Application.Common.Interfaces;

public interface IAuthService
{
    Task<BeginLoginResponseDto> BeginLoginAsync(string emailAddress);
    Task<CompleteLoginResponseDto> CompleteLoginAsync(string validationToken);
}