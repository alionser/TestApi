using Microsoft.AspNetCore.Mvc;
using TestAPI.Web.Commands.Employee;
using TestAPI.Web.Handlers.EmployeeCommandHandlers;

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