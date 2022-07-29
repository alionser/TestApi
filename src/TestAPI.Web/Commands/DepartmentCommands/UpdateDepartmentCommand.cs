using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands.DepartmentCommands;

public class UpdateDepartmentCommand : ICommand
{
    public int Id { get; set; }
    public string Name { get; set; }
}