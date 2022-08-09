using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.EmployeeCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<UpdateEmployeeCommand> _validator;

    public UpdateEmployeeCommandHandler(DataContext dataContext, IValidator<UpdateEmployeeCommand> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel> Handle(UpdateEmployeeCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"{nameof(command)} of {typeof(UpdateEmployeeCommand)} failed validation!");
        }

        var employee = await _dataContext.Employees
            .Include(x => x.Department)
            .FirstOrDefaultAsync(e => e.Id == command.Id, ct);

        if (employee == null)
        {
            throw new BadHttpRequestException($"{nameof(employee)} not found", (int)HttpStatusCode.NotFound);
        }

        var department = await _dataContext.Departments.FirstOrDefaultAsync(d => d.Id == command.DepartmentId, ct);
        if (department == null)
        {
            throw new BadHttpRequestException($"{nameof(department)} not found", (int)HttpStatusCode.NotFound);
        }

        employee.Name = command.Name.Trim();
        employee.Surname = command.Surname.Trim();
        employee.Patronymic = command.Patronymic.Trim();

        employee.Position = command.Position;
        employee.PhotoUri = command.PhotoUri;

        employee.Salary = command.Salary;
        employee.Age = command.Age;

        employee.Department = department;
        employee.DepartmentId = department.Id;

        await _dataContext.SaveChangesAsync(ct);
        return new ResponseModel();
    }
}