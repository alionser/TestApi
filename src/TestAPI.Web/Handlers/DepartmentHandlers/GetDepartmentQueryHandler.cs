using System.Net;
using FluentValidation;
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
    private readonly IValidator<GetDepartmentQuery> _validator;

    public GetDepartmentQueryHandler(DataContext dataContext, IValidator<GetDepartmentQuery> validator)
    {
        _dataContext = dataContext;
        _validator = validator;
    }

    public async Task<ResponseModel<GetDepartmentResultModel>> Handle(GetDepartmentQuery query, CancellationToken ct)
    {
        await _validator.ValidateAndThrowAsync(query, ct);

        var department = await _dataContext.Departments.FirstOrDefaultAsync(d => d.Id == query.Id, ct);

        if (department == null)
        {
            throw new BadHttpRequestException($"{nameof(department)} not found", (int)HttpStatusCode.NotFound);
        }

        var resultModel = new GetDepartmentResultModel
        {
            Id = department.Id,
            Name = department.Name
        };


        return new ResponseModel<GetDepartmentResultModel> { Result = resultModel };
    }
}