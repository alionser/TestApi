using TestAPI.Web.Data.Entities;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Extentsions;

public static class EmployeesQueryPagiantionExtension
{
    public static IQueryable<Employee> ApplyPagination(this IQueryable<Employee> employeesQuery,
        GetEmployeesQuery query)
    {
        if (query.Skip.HasValue)
        {
            employeesQuery = employeesQuery.Skip(query.Skip.Value);
        }

        if (query.Count.HasValue)
        {
            employeesQuery = employeesQuery.Take(query.Count.Value);
        }

        return employeesQuery;
    }
}