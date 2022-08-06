using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Data.Entities;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Queries;
using TestAPI.Web.ResponseModels;
using TestAPI.Web.ResponseModels.Employees;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class GetEmployeesQueryHandler : IQueryHandler<GetEmployeesQuery, GetEmployeesResultModel>
{
    private readonly DataContext _dataContext;

    public GetEmployeesQueryHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ResponseModel<GetEmployeesResultModel>> Handle(GetEmployeesQuery query, CancellationToken ct)
    {
        var employeesQuery = _dataContext.Employees.AsQueryable();
        var totalCount = await employeesQuery.CountAsync(ct);
        var employees = await ApplyFilter(employeesQuery, query)
            .Select(e => new ShortEmployModel
                {
                    Name = e.Name,
                    Surname = e.Surname,
                    Patronymic = e.Patronymic,

                    Salary = e.Salary
                }
            ).ToArrayAsync(ct);

        var countOnPage = employees.Length;

        var resultModel = new GetEmployeesResultModel
        {
            TotalCount = totalCount,
            CountOnPage = countOnPage,
            Employees = employees
        };
        return new ResponseModel<GetEmployeesResultModel> { Result = resultModel };
    }

    private static IQueryable<Employee> ApplyFilter(IQueryable<Employee> employeesQuery, GetEmployeesQuery query)
    {
        if (query.DepartmentId.HasValue)
        {
            employeesQuery = employeesQuery.Where(e => e.DepartmentId == query.DepartmentId.Value);
        }

        var normalizedInput = query.Surname?.Trim().ToUpperInvariant();
        if (!string.IsNullOrEmpty(normalizedInput))
        {
            employeesQuery = employeesQuery.Where(e => e.Surname.ToUpper() == normalizedInput);
        }

        if (query.Skip.HasValue)
        {
            employeesQuery = employeesQuery.Skip(query.Skip.Value);
        }

        if (query.Count.HasValue)
        {
            employeesQuery = employeesQuery.Skip(query.Count.Value);
        }

        return employeesQuery;
    }
}