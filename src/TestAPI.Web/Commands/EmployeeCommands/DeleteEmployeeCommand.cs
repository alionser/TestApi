using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands.EmployeeCommands;

public sealed class DeleteEmployeeCommand : ICommand
{
    public int Id { get; set; }
}