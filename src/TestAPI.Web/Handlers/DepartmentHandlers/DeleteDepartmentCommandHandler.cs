using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public sealed class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommand>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<DeleteDepartmentCommand> _validator;

    public DeleteDepartmentCommandHandler(DataContext dataContext, IValidator<DeleteDepartmentCommand> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel> Handle(DeleteDepartmentCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"{nameof(command)} of {typeof(DeleteDepartmentCommand)} failed validation!");
        }

        var department = await _dataContext.Departments.FirstOrDefaultAsync(d => d.Id == command.Id, ct);
        if (department == null)
        {
            throw new BadHttpRequestException($"{nameof(department)} not found", (int)HttpStatusCode.NotFound);
        }

        _dataContext.Departments.Remove(department);
        await _dataContext.SaveChangesAsync(ct);
        return new ResponseModel();
    }
}