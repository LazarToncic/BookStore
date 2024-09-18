using BookStore.Application.Common.Dto.Auth;
using BookStore.Application.Common.Interfaces;
using MediatR;

namespace BookStore.Application.Auth.Commands.BeginLoginCommand;

public record BeginLoginCommand(string EmailAddress) : IRequest<BeginLoginResponseDto>;

public class BeginLoginCommandHandler(IAuthService authService) : IRequestHandler<BeginLoginCommand, BeginLoginResponseDto>
{
    public async Task<BeginLoginResponseDto> Handle(BeginLoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.BeginLoginAsync(request.EmailAddress);
    }
}
