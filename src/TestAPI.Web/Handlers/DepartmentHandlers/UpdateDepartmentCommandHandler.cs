using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public sealed class UpdateDepartmentCommandHandler : ICommandHandler<UpdateDepartmentCommand>
{
    private readonly DataContext _dataContext;

    public UpdateDepartmentCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(UpdateDepartmentCommand command, CancellationToken ct)
    {
        var department = await _dataContext.Departments
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (department == null)
        {
            return new JsonResult("Failed");
        }

        department.Name = command.Name?.Trim();
        await _dataContext.SaveChangesAsync(ct);
        return new JsonResult(department);
    }
}