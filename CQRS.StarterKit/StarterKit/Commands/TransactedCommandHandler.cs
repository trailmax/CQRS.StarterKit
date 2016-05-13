using System;
using System.Reflection;
using System.Transactions;
using StarterKit.Logging;

namespace StarterKit.Commands
{
    /// <summary>
    /// Command handler decorator to add transaction around command handling.
    /// Checks if command has [TransactedCommand] attribute applied and starts transaction.
    /// If no attribute is applied, no transaction is started
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public class TransactedCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public ICommandHandler<TCommand> Decorated { get; set; }
        private readonly ILoggingService loggingService;

        public TransactedCommandHandler(
            ICommandHandler<TCommand> decorated, 
            ILoggingService loggingService)
        {
            Decorated = decorated;
            this.loggingService = loggingService;
        }


        public void Handle(TCommand command)
        {
            if (command.GetType().GetCustomAttribute<TransactedCommandAttribute>() == null)
            {
                Decorated.Handle(command);
                return;
            }


            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    Decorated.Handle(command);
                    scope.Complete();
                }
                catch (Exception exception)
                {
                    scope.Dispose();
                    loggingService.ErrorException("Failed to complete transaction", exception);
                    throw;
                }
            }
        }
    }
}
