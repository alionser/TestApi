using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands.DepartmentCommands;

public sealed class UpdateDepartmentCommand : ICommand
{
    public int Id { get; set; }
    public string Name { get; set; }
}