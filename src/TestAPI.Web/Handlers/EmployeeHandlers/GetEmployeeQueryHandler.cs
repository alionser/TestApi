using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery>
{
    private readonly DataContext _dataContext;

    public GetEmployeeQueryHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(GetEmployeeQuery query, CancellationToken ct)
    {
        var employee = await _dataContext.Employees.FirstOrDefaultAsync(e => e.Id == query.Id, ct);

        return new JsonResult(employee);
    }
}