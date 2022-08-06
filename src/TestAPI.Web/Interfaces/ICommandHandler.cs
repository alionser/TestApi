using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Interfaces;

public interface ICommandHandler<in TCommand> : IHandler
    where TCommand : ICommand
{
    public Task<ResponseModel> Handle(TCommand command, CancellationToken ct);
}