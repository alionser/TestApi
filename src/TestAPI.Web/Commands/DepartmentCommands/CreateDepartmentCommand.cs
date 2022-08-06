using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands.DepartmentCommands;

public sealed class CreateDepartmentCommand : ICommand
{
    public string Name { get; set; }
}