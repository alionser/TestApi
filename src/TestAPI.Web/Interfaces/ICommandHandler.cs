using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Web.Interfaces;

public interface IHandler
{
}

public interface ICommandHandler<in TCommand> : IHandler
    where TCommand : ICommand
{
    public Task<JsonResult> Handle(TCommand command, CancellationToken ct);
}

public interface IQueryHandler<in TQuery> : IHandler
    where TQuery : IQuery
{
    public Task<JsonResult> Handle(TQuery query, CancellationToken ct);
}