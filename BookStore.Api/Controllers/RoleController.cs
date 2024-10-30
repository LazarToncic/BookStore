using BookStore.Application.Roles.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

public class RoleController : ApiBaseController
{
    [Authorize(Roles = "Owner")]
    [HttpPost]
    public async Task<ActionResult> CreateRole(CreateRoleCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "StoreManager,Owner")]
    [HttpPut]
    public async Task<ActionResult> PromoteUserToEmployee([FromBody] ChangeRolesToEmployeeCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "Owner")]
    [HttpPut]
    public async Task<ActionResult> PromoteUserToStoreManager([FromBody] ChangeRolesToStoreManagerCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [Authorize(Roles = "Owner,StoreManager")]
    [HttpPut]
    public async Task<ActionResult> DemoteToCustomer([FromBody] DemoteRolesToCustomerCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}