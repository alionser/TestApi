using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands;

public sealed class DeleteDepartmentCommand : ICommand
{
    public int Id { get; set; }
}