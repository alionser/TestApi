using Microsoft.EntityFrameworkCore;
using TestAPI.Web.Data;
using TestAPI.Web.Interfaces;
using TestAPI.Web.Queries;
using TestAPI.Web.ResponseModels;
using TestAPI.Web.ResponseModels.Departments;

namespace TestAPI.Web.Handlers.DepartmentHandlers;

public sealed class GetDepartmentQueryHandler : IQueryHandler<GetDepartmentQuery, GetDepartmentResultModel>
{
    private readonly DataContext _dataContext;

    public GetDepartmentQueryHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ResponseModel<GetDepartmentResultModel>> Handle(GetDepartmentQuery query, CancellationToken ct)
    {
        var department = await _dataContext.Departments.FirstOrDefaultAsync(d => d.Id == query.Id, ct);

        if (department == null)
        {
            throw new Exception("Database error");
        }

        var resultModel = new GetDepartmentResultModel
        {
            Id = department.Id,
            Name = department.Name
        };


        return new ResponseModel<GetDepartmentResultModel> { Result = resultModel };
    }
}