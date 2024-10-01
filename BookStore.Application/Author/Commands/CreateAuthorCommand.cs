using BookStore.Application.Common.Dto.Author;
using BookStore.Application.Common.Exceptions;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappers.Author;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Author.Commands;

public record CreateAuthorCommand(CreateAuthorDto Dto) : IRequest;

public class CreateAuthorCommandHandler(IDemoDbContext dbContext, IAuthorService authorService) : IRequestHandler<CreateAuthorCommand>
{
    public async Task Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        await authorService.CreateAuthor(request.Dto, cancellationToken);
    }
} 