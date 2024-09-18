using BookStore.Application.Common.Interfaces;
using MediatR;

namespace BookStore.Application.Users.Commands;

public record CreateUserCommand(string EmailAddress, List<string> Roles, string FirstName, string LastName) : IRequest;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await userService.CreateUserAsync(request.EmailAddress, request.Roles, request.FirstName, request.LastName);
    }
} 