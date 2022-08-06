using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public class GetDepartmentQueryHandler : IQueryHandler<IQuery>
{
    private readonly DataContext _dataContext;

    public GetDepartmentQueryHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(IQuery query, CancellationToken ct)
    {
        var department = await _dataContext.Departments.FirstOrDefaultAsync(ct);

        return new JsonResult(department);
    }
}