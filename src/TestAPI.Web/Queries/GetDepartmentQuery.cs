using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Queries;

public class GetDepartmentQuery : IQuery
{
    public int Id { get; set; }
}