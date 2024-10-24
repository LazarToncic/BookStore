using BookStore.Application.Common.Dto.Auth;
using BookStore.Application.Common.Interfaces;
using MediatR;

namespace BookStore.Application.Auth.Commands.LoginCommand;

public record LoginCommand(LoginDto LoginDto) : IRequest<LoginResponseDto>;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request.LoginDto);
    }
} 