using BookStore.Application.Common.Dto.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Application.Common.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
    Task LogoutAsync();
}