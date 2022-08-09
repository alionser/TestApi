using FluentValidation;
using TestAPI.Web.Commands.DepartmentCommands;

namespace TestAPI.Web.Validators.Department;

public sealed class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);

        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty()
            .Length(1, 50);
    }
}