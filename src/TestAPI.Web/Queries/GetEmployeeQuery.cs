using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Queries;

public sealed class GetEmployeeQuery : IQuery
{
    public int Id { get; set; }
}