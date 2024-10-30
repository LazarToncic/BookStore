using BookStore.Application.Book.Commands;
using BookStore.Application.Book.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class BookController : ApiBaseController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetBookDetails([FromQuery] GetBookDetailsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetAllBooks([FromQuery] GetAllBooksQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    [Authorize(Roles = "Owner,StoreManager,Employee")]
    public async Task<ActionResult> CreateBook(CreateBookCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}