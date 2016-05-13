namespace StarterKit.Commands
{
    public interface ICommandValidator<in TCommand> where TCommand : ICommand 
    {
        ErrorList IsValid(TCommand command);
    }
}
