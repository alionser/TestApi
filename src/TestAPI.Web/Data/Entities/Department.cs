using System.Text.Json.Serialization;

namespace TestAPI.Web.Data.Entities;

public sealed class Department
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}