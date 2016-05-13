namespace StarterKit.Commands
{
    /// <summary>
    /// Not every command requires a validator. This pluggs in instead of a validator when command does not have it's own
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public class NullObjectCommandValidator<TCommand>
        : ICommandValidator<TCommand> where TCommand : ICommand 
    {
        public ErrorList IsValid(TCommand command)
        {
            return new ErrorList();
        }
    }
}
