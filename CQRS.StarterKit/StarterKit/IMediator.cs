using StarterKit.Commands;
using StarterKit.Queries;

namespace StarterKit
{
    /// <summary>
    /// Mediator - works as a router for command and queries.
    /// </summary>
    public interface IMediator
    {
        TResponse Request<TResponse>(IQuery<TResponse> query);
        ErrorList ProcessCommand<TCommand>(TCommand command) where TCommand : ICommand;
    }
}