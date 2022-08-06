using Microsoft.AspNetCore.Mvc;
using TestAPI.Web.Commands.EmployeeCommands;
using TestAPI.Web.Handlers.EmployeeHandlers;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Controllers;

[ApiController]
[Route("api/employees")]
public sealed class EmployeeController : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromServices] CreateEmployeeCommandHandler handler,
        [FromBody] CreateEmployeeCommand command,
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromServices] GetEmployeesQueryHandler handler,
        [FromQuery] GetEmployeesQuery query,
        CancellationToken ct)
    {
        return await handler.Handle(query, ct);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetEmployee([FromServices] GetEmployeeQueryHandler handler,
        [FromRoute] int id,
        CancellationToken ct)
    {
        var query = new GetEmployeeQuery
        {
            Id = id
        };

        return await handler.Handle(query, ct);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromServices] UpdateEmployeeCommandHandler handler,
        [FromBody] UpdateEmployeeCommand command,
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteEmployee([FromServices] DeleteEmployeeCommandHandler handler,
        [FromRoute] int id,
        CancellationToken ct)
    {
        var command = new DeleteEmployeeCommand()
        {
            Id = id
        };

        return await handler.Handle(command, ct);
    }
}