using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.EmployeeCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class
    DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
{
    private readonly DataContext _dataContext;

    public DeleteEmployeeCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ResponseModel> Handle(DeleteEmployeeCommand command, CancellationToken ct)
    {
        var deletedEmployee = await _dataContext.Employees.FirstOrDefaultAsync(e => e.Id == command.Id, ct);

        if (deletedEmployee == null)
        {
            throw new Exception();
        }

        _dataContext.Employees.Remove(deletedEmployee);
        await _dataContext.SaveChangesAsync(ct);

        return new ResponseModel();
    }
}