using BookStore.Application.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;
 
public class UserController : ApiBaseController
{
    [Authorize(Roles = "StoreManager,Owner")]
    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}