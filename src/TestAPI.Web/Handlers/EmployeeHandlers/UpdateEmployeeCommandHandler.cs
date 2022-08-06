using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.EmployeeCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
{
    private readonly DataContext _dataContext;

    public UpdateEmployeeCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ResponseModel> Handle(UpdateEmployeeCommand command, CancellationToken ct)
    {
        var employee = await _dataContext.Employees
            .FirstOrDefaultAsync(e => e.Id == command.Id, ct); //Не очень удачное имя, он еще не обновлен

        if (employee == null)
        {
            throw new Exception();
        }

        //вынести в отдельный метод
        employee.Name = command.Name.Trim(); //  может стоить добавить проверку свойств command на null?
        employee.Surname = command.Surname.Trim();
        employee.Patronymic = command.Patronymic.Trim();

        employee.Position = command.Position;
        employee.PhotoUri = command.PhotoUri;

        employee.Salary = command.Salary;
        employee.Age = command.Age;

        employee.DepartmentId = command.DepartmentId; //Что делать с навигационным свойством Department?
        employee.Department = await _dataContext.Departments.FirstOrDefaultAsync(d => d.Id == command.Id, ct);

        await _dataContext.SaveChangesAsync(ct);
        return new ResponseModel();
    }
}