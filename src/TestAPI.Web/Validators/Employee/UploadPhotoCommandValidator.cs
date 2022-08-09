using FluentValidation;
using TestAPI.Web.Commands.EmployeeCommands;

namespace TestAPI.Web.Validators.Employee;

public sealed class UploadPhotoCommandValidator : AbstractValidator<UploadPhotoCommand>
{
    public UploadPhotoCommandValidator()
    {
        RuleFor(c => c.EmployeeId)
            .GreaterThanOrEqualTo(0);

        //это вообще нужно? мы вручную проверяем в UploadPhotoCommandHandler
        RuleFor(c => c.Photo)
            .NotNull();
    }
}