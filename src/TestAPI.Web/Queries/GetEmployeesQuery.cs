using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Queries;

public sealed class GetEmployeesQuery : IQuery
{
    public string Surname { get; set; }
    public int? DepartmentId { get; set; }
    public int? Skip { get; set; }
    public int? Count { get; set; }
}