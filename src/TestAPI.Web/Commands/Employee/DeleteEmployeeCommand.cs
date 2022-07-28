using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands.Employee;

public sealed class DeleteEmployeeCommand : ICommand
{
    public int Id { get; set; }
}