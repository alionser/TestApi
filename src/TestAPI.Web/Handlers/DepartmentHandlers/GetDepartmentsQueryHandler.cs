using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Queries;
using TestAPI.Web.ResponseModels;
using TestAPI.Web.ResponseModels.Departments;
using TestAPI.Web.ResponseModels.Employees;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public sealed class GetDepartmentsQueryHandler : IQueryHandler<GetDepartmentsQuery, GetDepartmentsResultModel>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<GetDepartmentsQuery> _validator;

    public GetDepartmentsQueryHandler([FromServices] DataContext dataContext, IValidator<GetDepartmentsQuery> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel<GetDepartmentsResultModel>> Handle(GetDepartmentsQuery query, CancellationToken ct)
    {
        await _validator.ValidateAndThrowAsync(query, ct);
        var departments = await _dataContext.Departments
            .Select(x => new DepartmentsListItem
            {
                Name = x.Name,
                Id = x.Id,
                Employees = x.Employees
                    .Select(e => new ShortEmployModel
                    {
                        Name = e.Name,
                        Surname = e.Surname,
                        Patronymic = e.Patronymic,
                        Salary = e.Salary
                    })
                    .ToArray(),
                EmployeeCount = x.Employees.Count,
                SumSalary = x.Employees.Sum(emp => emp.Salary)
            })
            .ToArrayAsync(ct);

        var resultModel = new GetDepartmentsResultModel { Departments = departments };

        return new ResponseModel<GetDepartmentsResultModel> { Result = resultModel };
    }
}