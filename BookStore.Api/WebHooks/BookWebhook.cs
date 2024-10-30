using BookStore.Application.Book.Commands;
using Demo.Api.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.WebHooks;

[Authorize(AuthenticationSchemes = nameof(AuthConstants.HeaderBasicAuthenticationScheme))]
public class BookWebhook : BaseWebHook
{
    [HttpPost]
    public async Task<ActionResult> CreateBook(CreateBookCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}