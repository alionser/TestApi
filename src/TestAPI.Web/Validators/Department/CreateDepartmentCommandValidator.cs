using FluentValidation;
using TestAPI.Web.Commands.DepartmentCommands;

namespace TestAPI.Web.Validators.Department;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty()
            .Length(1, 50);
    }
}