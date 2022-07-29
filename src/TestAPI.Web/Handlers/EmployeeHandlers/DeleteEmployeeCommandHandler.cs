using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.EmployeeCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class
    DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand> //TODO: подумать над наслдованием, у всех хендлеров одинаковый конструтор
{
    private readonly DataContext _dataContext;

    public DeleteEmployeeCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(DeleteEmployeeCommand command, CancellationToken ct)
    {
        var deletedEmployee = await _dataContext.Employees.FirstOrDefaultAsync(e => e.Id == command.Id, ct);

        if (deletedEmployee == null)
            return new JsonResult($"Failed to find Employee with Id:{command.Id}");

        _dataContext.Employees.Remove(deletedEmployee);
        await _dataContext.SaveChangesAsync(ct);

        //TODO: нормальный кастомный IActionResult с json и статус кодом
        return new JsonResult($"Ok, Employee with Id: {command.Id} was deleted.");
    }
}