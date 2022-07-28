using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Web.Interfaces;

public interface ICommandHandler<in TCommand> : IHandler
    where TCommand : ICommand
{
    public Task<JsonResult> Handle(TCommand command, CancellationToken ct);
}