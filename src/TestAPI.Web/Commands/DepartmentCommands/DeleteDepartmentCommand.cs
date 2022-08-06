using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands.DepartmentCommands;

public sealed class DeleteDepartmentCommand : ICommand
{
    public int Id { get; set; }
}