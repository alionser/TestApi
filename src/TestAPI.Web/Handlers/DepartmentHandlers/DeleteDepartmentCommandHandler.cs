﻿using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.DepartmentCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommand>
{
    private readonly DataContext _dataContext;

    public DeleteDepartmentCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ResponseModel> Handle(DeleteDepartmentCommand command, CancellationToken ct)
    {
        var department = await _dataContext.Departments.FirstOrDefaultAsync(d => d.Id == command.Id, ct);
        if (department == null)
        {
            throw new Exception();
        }

        _dataContext.Departments.Remove(department);
        await _dataContext.SaveChangesAsync(ct);
        return new ResponseModel();
    }
}