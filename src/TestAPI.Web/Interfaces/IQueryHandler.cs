using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Interfaces;

public interface IQueryHandler<in TQuery, TResult> : IHandler
    where TQuery : IQuery
{
    public Task<ResponseModel<TResult>> Handle(TQuery query, CancellationToken ct);
}