using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Web.Interfaces;

public interface IQueryHandler<in TQuery> : IHandler
    where TQuery : IQuery
{
    public Task<JsonResult> Handle(TQuery query, CancellationToken ct);
}