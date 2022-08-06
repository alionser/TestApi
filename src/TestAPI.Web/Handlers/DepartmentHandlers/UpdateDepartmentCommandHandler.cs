using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public sealed class UpdateDepartmentCommandHandler : ICommandHandler<UpdateDepartmentCommand>
{
    private readonly DataContext _dataContext;

    public UpdateDepartmentCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ResponseModel> Handle(UpdateDepartmentCommand command, CancellationToken ct)
    {
        var department = await _dataContext.Departments
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (department == null)
        {
            throw new Exception();
        }

        department.Name = command.Name?.Trim();
        await _dataContext.SaveChangesAsync(ct);
        return new ResponseModel();
    }
}