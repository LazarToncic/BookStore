using BookStore.Application.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;
 
public class UserController : ApiBaseController
{
    //[Authorize]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUserCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}