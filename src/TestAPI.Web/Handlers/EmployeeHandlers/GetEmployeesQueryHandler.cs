using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class GetEmployeesQueryHandler : IQueryHandler<GetEmployeesQuery>
{
    private readonly DataContext _dataContext;

    public GetEmployeesQueryHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(GetEmployeesQuery query, CancellationToken ct)
    {
        var employees = await _dataContext.Employees.ToArrayAsync(ct);
        return new JsonResult(employees);
    }
}