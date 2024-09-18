using BookStore.Api.Auth.Constants;
using BookStore.Application.Book.Commands;
using BookStore.Application.Book.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class BookController : ApiBaseController
{
    [HttpGet]
    //[Authorize(AuthenticationSchemes = nameof(AuthConstants.HeaderBasicAuthenticationScheme))]
    public async Task<ActionResult> GetBookDetails([FromQuery] GetBookDetailsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllBooks([FromQuery] GetAllBooksQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateBook(CreateBookCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}