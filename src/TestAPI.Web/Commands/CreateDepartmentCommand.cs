using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands;

public sealed class CreateDepartmentCommand : ICommand
{
    public string Name { get; set; }
}