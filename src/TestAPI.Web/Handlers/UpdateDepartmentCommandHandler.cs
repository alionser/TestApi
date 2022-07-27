using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Handlers;

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

        department.Name = command.Name.Trim();
        return new JsonResult(department);
    }
}