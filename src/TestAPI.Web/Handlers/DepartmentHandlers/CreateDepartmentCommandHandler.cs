using FluentValidation;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Data.Entities;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

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
        await _validator.ValidateAndThrowAsync(command, ct);

        var department = new Department { Name = command.Name };
        await _dataContext.Departments.AddAsync(department, ct);
        await _dataContext.SaveChangesAsync(ct);
        return new ResponseModel();
    }
}