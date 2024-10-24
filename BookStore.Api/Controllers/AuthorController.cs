using BookStore.Application.Author.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Authorize(Roles = "Employee,Owner,StoreManager")]
public class AuthorController : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreateAuthor(CreateAuthorCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}