using FluentValidation;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Validators.Employee;

public sealed class GetEmployeesQueryValidator : AbstractValidator<GetEmployeesQuery>
{
    public GetEmployeesQueryValidator()
    {
        RuleFor(q => q.Surname)
            .Length(1, 50)
            .When(q => q.Surname != null);

        RuleFor(q => q.DepartmentId)
            .GreaterThanOrEqualTo(0)
            .When(q => q.DepartmentId.HasValue);

        RuleFor(q => q.Skip)
            .GreaterThanOrEqualTo(0)
            .When(q => q.Skip.HasValue);

        RuleFor(q => q.Count)
            .GreaterThanOrEqualTo(0)
            .When(q => q.Count.HasValue);
    }
}