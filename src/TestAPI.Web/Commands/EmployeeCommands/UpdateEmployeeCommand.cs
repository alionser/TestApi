using TestAPI.Web.Data.Entities;
using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands.EmployeeCommands;

public sealed class UpdateEmployeeCommand : ICommand
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }

    public Position Position { get; set; }

    public Uri PhotoUri { get; set; }

    public decimal Salary { get; set; }
    public int Age { get; set; }

    public int DepartmentId { get; set; }
}