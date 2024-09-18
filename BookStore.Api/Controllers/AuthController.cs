using BookStore.Application.Auth.Commands.BeginLoginCommand;
using BookStore.Application.Auth.Commands.CompleteLoginCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class AuthController : ApiBaseController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> BeginLogin(BeginLoginCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [AllowAnonymous]
    [HttpGet("{validationToken}/CompleteLogin")]
    public async Task<ActionResult> CompleteLogin([FromRoute] string validationToken)
    {
        return Ok(await Mediator.Send(new CompleteLoginCommand(validationToken)));
    }
}