using BookStore.Application.Common.Dto.Auth;
using BookStore.Application.Common.Interfaces;
using MediatR;

namespace BookStore.Application.Auth.Commands.CompleteLoginCommand;

public record CompleteLoginCommand(string ValidationToken) : IRequest<CompleteLoginResponseDto>;

public class BeginLoginCommandHandler(IAuthService authService) : IRequestHandler<CompleteLoginCommand, CompleteLoginResponseDto>
{
    public async Task<CompleteLoginResponseDto> Handle(CompleteLoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.CompleteLoginAsync(request.ValidationToken);
    }
}
