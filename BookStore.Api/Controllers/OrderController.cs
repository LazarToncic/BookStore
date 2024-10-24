using BookStore.Application.Order.Commands;
using BookStore.Application.Order.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class OrderController : ApiBaseController
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateOrder(CreateOrderCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetOrdersForSingleUser()
    {
        var result = await Mediator.Send(new GetOrdersForSingleUserQuery());
        return Ok(result);
    }
}