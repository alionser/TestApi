using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.EmployeeCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class UploadPhotoCommandHandler : ICommandHandler<UploadPhotoCommand>
{
    private readonly DataContext _dataContext;

    public UploadPhotoCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ResponseModel> Handle(UploadPhotoCommand command, CancellationToken ct)
    {
        var employee = await _dataContext.Employees.FirstOrDefaultAsync(e => e.Id == command.EmployeeId, ct);

        if (employee == null)
        {
            throw new Exception();
        }

        var file = new MemoryStream();
        
        if (command.Photo == null)
        {
            throw new Exception();
        }
        
        await command.Photo.CopyToAsync(file, ct);
        
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "files");
        var fullFilePath = Path.Combine(directoryPath, command.Photo.FileName);

        Directory.CreateDirectory(directoryPath);
        await using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
        {
            file.Position = 0;
            await file.CopyToAsync(fileStream, ct);
        }

        employee.PhotoUri = new Uri(fullFilePath);
        await _dataContext.SaveChangesAsync(ct);

        return new ResponseModel();
    }
}