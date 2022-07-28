using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.Employee;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
{
    private readonly DataContext _dataContext;

    public UpdateEmployeeCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(UpdateEmployeeCommand command, CancellationToken ct)
    {
        var updatedEmployee = await _dataContext.Employees.
            FirstOrDefaultAsync(e => e.Id == command.Id, ct); //Не очень удачное имя, он еще не обновлен

        if (updatedEmployee == null)
            return new JsonResult($"Employee with Id: {command.Id} not found"); //грамматика!

        //вынести в отдельный метод
        updatedEmployee.Name = command.Name.Trim(); //  может стоить добавить проверку свойств command на null?
        updatedEmployee.Surname = command.Surname.Trim();
        updatedEmployee.Patronymic = command.Patronymic.Trim();

        updatedEmployee.Position = command.Position;
        updatedEmployee.PhotoUri = command.PhotoUri;
        
        updatedEmployee.Salary = command.Salary;
        updatedEmployee.Age = command.Age;

        updatedEmployee.DepartmentId = command.DepartmentId; //Что делать с навигационным свойством Department?
        updatedEmployee.Department = await _dataContext.Departments.
            FirstOrDefaultAsync(d => d.Id == command.Id, ct);

        await _dataContext.SaveChangesAsync(ct);
        return new JsonResult(updatedEmployee);
    }
}