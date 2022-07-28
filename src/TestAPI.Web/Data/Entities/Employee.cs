using System.Text.Json.Serialization;

namespace TestAPI.Web.Data.Entities;

public sealed class Employee
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }

    public Position Position { get; set; }

    public Uri PhotoUri { get; set; }

    public decimal Salary { get; set; }
    public int Age { get; set; }

    [JsonIgnore]
    public Department Department { get; set; }
    public int DepartmentId { get; set; }
}