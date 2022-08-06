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

    public GetEmployeeQueryHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ResponseModel<GetEmployeeResultModel>> Handle(GetEmployeeQuery query, CancellationToken ct)
    {
        var employee = await _dataContext.Employees.FirstOrDefaultAsync(e => e.Id == query.Id, ct);

        if (employee == null)
        {
            throw new Exception();
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