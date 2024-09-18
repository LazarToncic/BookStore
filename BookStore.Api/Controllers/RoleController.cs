using BookStore.Application.Roles.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class RoleController : ApiBaseController
{
    //[Authorize(Roles = "Owner")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> CreateRole(CreateRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}