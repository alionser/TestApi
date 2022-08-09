using FluentValidation;
using FluentValidation.Results;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Data.Entities;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;
using TestAPI.Web.Validators.Department;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public sealed class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<CreateDepartmentCommand> _validator;

    public CreateDepartmentCommandHandler(DataContext dataContext, IValidator<CreateDepartmentCommand> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel> Handle(CreateDepartmentCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"{nameof(command)} of {typeof(CreateDepartmentCommand)} failed validation!");
        }
        
        var department = new Department { Name = command.Name };
        await _dataContext.Departments.AddAsync(department, ct);
        await _dataContext.SaveChangesAsync(ct);
        return new ResponseModel();
    }
}