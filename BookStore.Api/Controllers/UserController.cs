using BookStore.Application.Users.Commands;
using BookStore.Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;
 
[Authorize(Roles = "StoreManager,Owner,Employee")]
public class UserController : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers([FromQuery] GetAllUsersQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}