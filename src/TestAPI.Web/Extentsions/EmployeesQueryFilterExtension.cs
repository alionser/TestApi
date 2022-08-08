using TestAPI.Web.Data.Entities;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Extentsions;

public static class EmployeesQueryFilterExtension
{
    public static IQueryable<Employee> ApplyFilter(this IQueryable<Employee> employeesQuery,
        GetEmployeesQuery query)
    {
        if (query.DepartmentId.HasValue)
        {
            employeesQuery = employeesQuery.Where(e => e.DepartmentId == query.DepartmentId.Value);
        }

        var normalizedInput = query.Surname?.Trim().ToUpperInvariant();
        if (!string.IsNullOrEmpty(normalizedInput))
        {
            employeesQuery = employeesQuery.Where(e => e.Surname.ToUpper().Contains(normalizedInput));
        }

        return employeesQuery;
    }
}