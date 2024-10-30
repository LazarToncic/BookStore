using BookStore.Application.LoyaltyProgram.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;
[Authorize(Roles = "Owner,StoreManager,Employee")]
public class LoyaltyProgramController : ApiBaseController
{
    [HttpPut]
    public async Task<ActionResult> SetLoyaltyProgramToDefaultForAllUsers()
    {
        await Mediator.Send(new SetLoyaltyProgramToDefaultForAllUsersCommand());
        return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult> SetLoyaltyProgramToDefaultForSingleUser([FromBody] SetLoyaltyProgramToDefaultForSingleUserCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}