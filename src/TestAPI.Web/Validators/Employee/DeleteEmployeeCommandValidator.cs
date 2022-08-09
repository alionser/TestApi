using FluentValidation;
using TestAPI.Web.Commands.EmployeeCommands;

namespace TestAPI.Web.Validators.Employee;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThanOrEqualTo(0);
    }
}