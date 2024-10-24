using BookStore.Application.BookCategory.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class BookCategoryController : ApiBaseController
{
    [HttpPost]
    [Authorize(Roles = "Owner,StoreManager,Employee")]
    public async Task<ActionResult> CreateBookCategory(CreateBookCategoryCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}