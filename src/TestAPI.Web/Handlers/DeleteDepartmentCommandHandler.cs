using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Handlers;

public class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommand>
{
    private readonly DataContext _dataContext;

    public DeleteDepartmentCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(DeleteDepartmentCommand command, CancellationToken ct)
    {
        var department = await _dataContext.Departments.FirstOrDefaultAsync(d => d.Id == command.Id, ct);
        if (department == null)
        {
            return new JsonResult("Failed");
        }

        _dataContext.Departments.Remove(department);
        await _dataContext.SaveChangesAsync(ct);
        return new JsonResult("Ok");
    }
}