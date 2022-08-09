using FluentValidation;
using TestAPI.Web.Commands.DepartmentCommands;

namespace TestAPI.Web.Validators.Department;

public sealed class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);
    }
}