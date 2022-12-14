using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Commands.EmployeeCommands;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class UploadPhotoCommandHandler : ICommandHandler<UploadPhotoCommand>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<UploadPhotoCommand> _validator;

    public UploadPhotoCommandHandler(DataContext dataContext, IValidator<UploadPhotoCommand> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel> Handle(UploadPhotoCommand command, CancellationToken ct)
    {
        await _validator.ValidateAndThrowAsync(command, ct);
        
        var employee = await _dataContext.Employees.FirstOrDefaultAsync(e => e.Id == command.EmployeeId, ct);

        if (employee == null)
        {
            throw new BadHttpRequestException($"{nameof(employee)} not found", (int)HttpStatusCode.NotFound);
        }

        var file = new MemoryStream();

        if (command.Photo == null)
        {
            throw new BadHttpRequestException($"{nameof(file)} not uploaded");
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