using FluentValidation;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Validators.Department;

public sealed class GetDepartmentQueryValidator : AbstractValidator<GetDepartmentQuery>
{
    public GetDepartmentQueryValidator()
    {
        RuleFor(q => q.Id)
            .GreaterThan(0);
    }
}