using BookStore.Application.Common.Interfaces;
using MediatR;

namespace BookStore.Application.Auth.Commands.LogoutCommand;

public record LogoutCommand() : IRequest<Unit>;

public class LogoutCommandHandler(IAuthService authService) : IRequestHandler<LogoutCommand, Unit>
{
    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    { 
        await authService.LogoutAsync();
        return Unit.Value;
    }
} 