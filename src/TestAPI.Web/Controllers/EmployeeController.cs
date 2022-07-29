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
    public async Task<IActionResult> GetEmployees([FromServices] GetEmployeesQueryHandler handler, //нужно ли действие для получения единственного Employee
        [FromQuery] GetEmployeesQuery query,
        CancellationToken ct)
    {
        return await handler.Handle(query, ct);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromServices] UpdateEmployeeCommandHandler handler,
        [FromBody] UpdateEmployeeCommand command,
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    [HttpDelete, Route("{id:int}")]
    public async Task<IActionResult> DeleteEmployee([FromServices] DeleteEmployeeCommandHandler handler,
        [FromRoute] int id, //Можно ли параметры сразу к команде привязать?
        CancellationToken ct)
    {
        var command = new DeleteEmployeeCommand()
        {
            Id = id
        };
        
        return await handler.Handle(command, ct);
    }
}