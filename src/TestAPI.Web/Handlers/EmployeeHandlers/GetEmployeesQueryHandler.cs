using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Extentsions;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Queries;
using TestAPI.Web.ResponseModels;
using TestAPI.Web.ResponseModels.Employees;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class GetEmployeesQueryHandler : IQueryHandler<GetEmployeesQuery, GetEmployeesResultModel>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<GetEmployeesQuery> _validator;

    public GetEmployeesQueryHandler(DataContext dataContext, IValidator<GetEmployeesQuery> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel<GetEmployeesResultModel>> Handle(GetEmployeesQuery query, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(query, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"{nameof(query)} of {typeof(GetEmployeesQuery)} failed validation!");
        }

        var employeesQuery = _dataContext.Employees.AsQueryable();
        var totalCount = await employeesQuery.CountAsync(ct);
        var employees = await employeesQuery
            .ApplyFilter(query)
            .ApplyPagination(query)
            .Select(e => new ShortEmployModel
                {
                    Name = e.Name,
                    Surname = e.Surname,
                    Patronymic = e.Patronymic,

                    Salary = e.Salary
                }
            ).ToArrayAsync(ct);

        var countOnPage = employees.Length;

        var resultModel = new GetEmployeesResultModel
        {
            TotalCount = totalCount,
            CountOnPage = countOnPage,
            Employees = employees
        };
        return new ResponseModel<GetEmployeesResultModel> { Result = resultModel };
    }
}