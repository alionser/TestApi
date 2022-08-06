using TestAPI.Web.ResponseModels.Employees;

namespace TestAPI.Web.ResponseModels.Departments;

public sealed class DepartmentsListItem
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<ShortEmployModel> Employees { get; set; } = new List<ShortEmployModel>();

    public int EmployeeCount { get; set; }
    public decimal SumSalary { get; set; }
}