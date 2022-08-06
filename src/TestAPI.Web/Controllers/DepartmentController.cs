using Microsoft.AspNetCore.Mvc;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Handlers.DepartmentHandlers;
using TestAPI.Web.Queries;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Controllers;

[ApiController]
[Route("api/departments")]
public sealed class DepartmentController : Controller
{
    [HttpPost]
    public async Task<ResponseModel> CreateDepartment([FromServices] CreateDepartmentCommandHandler handler,
        [FromBody] CreateDepartmentCommand command,
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    [HttpGet]
    public async Task<ResponseModel> GetDepartments([FromServices] GetDepartmentsQueryHandler handler,
        [FromQuery] GetDepartmentsQuery query,
        CancellationToken ct)
    {
        return await handler.Handle(query, ct);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ResponseModel> GetDepartment([FromServices] GetDepartmentQueryHandler handler,
        [FromRoute] int id,
        CancellationToken ct)
    {
        var query = new GetDepartmentQuery { Id = id };

        return await handler.Handle(query, ct);
    }

    [HttpPut]
    public async Task<ResponseModel> UpdateDepartment([FromServices] UpdateDepartmentCommandHandler handler,
        [FromBody] UpdateDepartmentCommand command,
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    [HttpDelete, Route("{id:int}")]
    public async Task<ResponseModel> DeleteDepartment([FromServices] DeleteDepartmentCommandHandler handler,
        [FromRoute] int id, CancellationToken ct)
    {
        var command = new DeleteDepartmentCommand
        {
            Id = id
        };

        return await handler.Handle(command, ct);
    }
}