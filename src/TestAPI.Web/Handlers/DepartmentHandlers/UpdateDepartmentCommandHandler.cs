using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public sealed class UpdateDepartmentCommandHandler : ICommandHandler<UpdateDepartmentCommand>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<UpdateDepartmentCommand> _validator;

    public UpdateDepartmentCommandHandler(DataContext dataContext, IValidator<UpdateDepartmentCommand> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel> Handle(UpdateDepartmentCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"{nameof(command)} of {typeof(UpdateDepartmentCommand)} failed validation!");
        }

        var department = await _dataContext.Departments
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (department == null)
        {
            throw new BadHttpRequestException($"{nameof(department)} not found", (int)HttpStatusCode.NotFound);
        }

        department.Name = command.Name?.Trim();
        await _dataContext.SaveChangesAsync(ct);
        return new ResponseModel();
    }
}