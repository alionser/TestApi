using FluentValidation;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Validators.Employee;

public sealed class GetEmployeeQueryValidator : AbstractValidator<GetEmployeeQuery>
{
    public GetEmployeeQueryValidator()
    {
        RuleFor(q => q.Id)
            .GreaterThanOrEqualTo(0);
    }
}