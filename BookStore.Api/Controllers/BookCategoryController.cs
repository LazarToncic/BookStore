using BookStore.Application.BookCategory.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class BookCategoryController : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreateBookCategory(CreateBookCategoryCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}