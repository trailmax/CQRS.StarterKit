using System;
using Newtonsoft.Json;
using StarterKit.Logging;

namespace StarterKit.Commands
{
    /// <summary>
    /// Decorator for command handlers to add logging around command processing
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public class LoggedCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public ICommandHandler<TCommand> Decorated { get; set; }
        private readonly ILoggingService logger;

        public LoggedCommandHandlerDecorator(ICommandHandler<TCommand> decorated, ILoggingService logger)
        {
            Decorated = decorated;
            this.logger = logger;
            logger.SetLoggerName("Command Handler");
        }


        public void Handle(TCommand command)
        {
            String serialisedData;
            try
            {
                serialisedData = JsonConvert.SerializeObject(command, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception)
            {
                serialisedData = "Unable to serialize command";
            }

            logger.Info("About to handle command handler of type {0} with data {1}", command.GetType().Name, serialisedData);

            Decorated.Handle(command);

            logger.Info("Finished with command handler of type {0}", command.GetType().Name);
        }
    }
}
