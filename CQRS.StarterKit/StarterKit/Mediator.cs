using SimpleInjector;
using StarterKit.Commands;
using StarterKit.Queries;

namespace StarterKit
{
    /// <summary>
    /// Implementation of Mediator that works with SimpleInjector
    /// But very much the same implementation will be for any other DI container.
    /// </summary>
    public class Mediator : IMediator
    {
        private readonly Container container;

        public Mediator(Container container)
        {
            this.container = container;
        }

        public TResponseData Request<TResponseData>(IQuery<TResponseData> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponseData));
            var handler = container.GetInstance(handlerType);
            var result = (TResponseData)handler.GetType().GetMethod("Handle", new[] { query.GetType() }).Invoke(handler, new object[] { query });
            return result;
        }


        public ErrorList ProcessCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var validator = container.GetInstance<ICommandValidator<TCommand>>();

            var errors = validator.IsValid(command);
            if (!errors.IsValid())
            {
                return errors;
            }

            var handler = container.GetInstance<ICommandHandler<TCommand>>();

            handler.Handle(command);

            return new ErrorList();
        }
    }
}