using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Handlers;

public class GetDepartmentsQueryHandler : IQueryHandler<GetDepartmentsQuery>
{
    private readonly DataContext _dataContext;

    public GetDepartmentsQueryHandler([FromServices] DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<JsonResult> Handle(GetDepartmentsQuery query, CancellationToken ct)
    {
        var departments = await _dataContext.Departments
            .Select(x => new
            {
                Name = x.Name,
                Id = x.Id,
                Employees = x.Employees
                    .Select(e => new
                    {
                        Employ = string.Join(" ", new { e.Name, e.Surname, e.Patronymic }).Trim(),
                        Salary = e.Salary
                    }),
                EmployeesCount = x.Employees.Count,
                SumSalary = x.Employees.Sum(emp => emp.Salary)
            })
            .ToArrayAsync(ct);

        return new JsonResult(departments);
    }
}