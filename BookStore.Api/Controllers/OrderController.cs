using BookStore.Application.Order.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class OrderController : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}