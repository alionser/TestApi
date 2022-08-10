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
        await _validator.ValidateAndThrowAsync(command, ct);

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