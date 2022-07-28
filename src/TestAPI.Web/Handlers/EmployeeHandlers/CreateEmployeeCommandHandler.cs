using Microsoft.AspNetCore.Mvc;
using TestAPI.Web.Commands.Employee;
using TestAPI.Web.Data;
using TestAPI.Web.Data.Entities;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
{
    private readonly DataContext _dataContext;

    public CreateEmployeeCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(CreateEmployeeCommand command, CancellationToken ct)
    {
        var employee = new Employee
        {
            Name = command.Name,
            Surname = command.Surname,
            Patronymic = command.Patronymic,
            
            Position = command.Position,
            PhotoUri = command.PhotoUri,
            
            Salary = command.Salary,
            Age = command.Age,
            
            DepartmentId = command.DepartmentId
        };

        await _dataContext.AddAsync(employee, ct);
        await _dataContext.SaveChangesAsync(ct);
        return new JsonResult(employee);
    }
}