using BookStore.Application.Common.Dto.Auth;
using BookStore.Application.Common.Interfaces;
using MediatR;

namespace BookStore.Application.Auth.Commands.RegisterCommand;

public record RegisterCommand(RegisterDto RegisterDto) : IRequest;

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand>
{
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await authService.RegisterAsync(request.RegisterDto);
    }
} 