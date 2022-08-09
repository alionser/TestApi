using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.EmployeeCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<DeleteEmployeeCommand> _validator;

    public DeleteEmployeeCommandHandler(DataContext dataContext, IValidator<DeleteEmployeeCommand> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel> Handle(DeleteEmployeeCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"{nameof(command)} of {typeof(DeleteEmployeeCommand)} failed validation!");
        }
        
        var employee = await _dataContext.Employees.FirstOrDefaultAsync(e => e.Id == command.Id, ct);

        if (employee == null)
        {
            throw new BadHttpRequestException($"{nameof(employee)} not found", (int)HttpStatusCode.NotFound);
        }

        _dataContext.Employees.Remove(employee);
        await _dataContext.SaveChangesAsync(ct);

        return new ResponseModel();
    }
}