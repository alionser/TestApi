using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Queries;

public sealed class GetEmployeesQuery : IQuery //Совпадают Query - параметры запроса и Query - концепция CQS
{
    public string Surname { get; set; } //TODO: разобраться с рагистром в запросе
    public int? DepartmentId { get; set; }
}