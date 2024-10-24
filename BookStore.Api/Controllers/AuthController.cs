using BookStore.Application.Auth.Commands.LoginCommand;
using BookStore.Application.Auth.Commands.LogoutCommand;
using BookStore.Application.Auth.Commands.RegisterCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class AuthController : ApiBaseController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await Mediator.Send(new LogoutCommand());
        return Ok("Logged out successfully.");
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return Ok(new { message = "Access denied. You do not have permission to access this resource." });
    }
    
    
}