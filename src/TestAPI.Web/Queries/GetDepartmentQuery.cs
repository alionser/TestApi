using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Queries;

public sealed class GetDepartmentQuery : IQuery
{
    public int Id { get; set; }
}