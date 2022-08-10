using FluentValidation;
using TestAPI.Web.Commands.EmployeeCommands;

namespace TestAPI.Web.Validators.Employee;

public sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty()
            .Length(1, 50);

        RuleFor(c => c.Surname)
            .NotNull()
            .NotEmpty()
            .Length(1, 50);

        RuleFor(c => c.Patronymic)
            .NotNull()
            .NotEmpty()
            .Length(1, 50);

        RuleFor(c => c.Age)
            .InclusiveBetween(18, 120);

        RuleFor(c => c.Salary)
            .ScalePrecision(18, 2)
            .GreaterThanOrEqualTo(0.0m);

        RuleFor(c => c.Position)
            .IsInEnum();

        RuleFor(c => c.DepartmentId)
            .GreaterThanOrEqualTo(0);
    }
}