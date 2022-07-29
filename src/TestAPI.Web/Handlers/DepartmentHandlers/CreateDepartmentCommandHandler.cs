using Microsoft.AspNetCore.Mvc;
using TestAPI.Web.Commands;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Data.Entities;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand>
{
    private readonly DataContext _dataContext;

    public CreateDepartmentCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(CreateDepartmentCommand command, CancellationToken ct)
    {
        var department = new Department { Name = command.Name };
        await _dataContext.Departments.AddAsync(department, ct);
        await _dataContext.SaveChangesAsync(ct);
        return new JsonResult(department);
    }
}