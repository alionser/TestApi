using FluentValidation;
using TestAPI.Web.Queries;

namespace TestAPI.Web.Validators.Department;

public sealed class GetDepartmentsQueryValidator : AbstractValidator<GetDepartmentsQuery>
{
    public GetDepartmentsQueryValidator()
    {
    }
}