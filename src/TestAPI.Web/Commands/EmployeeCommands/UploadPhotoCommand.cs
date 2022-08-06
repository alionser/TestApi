using TestAPI.Web.Interfaces;

namespace TestAPI.Web.Commands.EmployeeCommands;

public sealed class UploadPhotoCommand : ICommand
{
    public int EmployeeId { get; set; }
    
    public IFormFile Photo { get; set; }
}