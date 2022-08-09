using System.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Queries;
using TestAPI.Web.ResponseModels;
using TestAPI.Web.ResponseModels.Employees;

namespace TestAPI.Web.Handlers.EmployeeHandlers;

public sealed class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, GetEmployeeResultModel>
{
    private readonly DataContext _dataContext;
    private readonly IValidator<GetEmployeeQuery> _validator;

    public GetEmployeeQueryHandler(DataContext dataContext, IValidator<GetEmployeeQuery> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel<GetEmployeeResultModel>> Handle(GetEmployeeQuery query, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(query, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"{nameof(query)} of {typeof(GetEmployeeQuery)} failed validation!");
        }
        
        var employee = await _dataContext.Employees.FirstOrDefaultAsync(e => e.Id == query.Id, ct);

        if (employee == null)
        {
            throw new BadHttpRequestException($"{nameof(employee)} not found", (int)HttpStatusCode.NotFound);
        }

        var employeeResultModel = new GetEmployeeResultModel
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Patronymic = employee.Patronymic,
            Salary = employee.Salary,
            PhotoUrl = employee.PhotoUri.ToString()
        };

        return new ResponseModel<GetEmployeeResultModel> { Result = employeeResultModel };
    }
}