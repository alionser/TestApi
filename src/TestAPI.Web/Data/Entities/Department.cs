using System.Text.Json.Serialization;

namespace TestAPI.Web.Data.Entities;

public sealed class Department
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}