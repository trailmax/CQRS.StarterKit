using System;
using StarterKit.Commands;

namespace StarterKit.Samples
{
    public class ValidatedCommand : ICommand
    {
        public ValidatedCommand(bool amValid)
        {
            IAmValid = amValid;
        }

        public bool IAmValid { get; private set; }
    }

    public class ValidatedCommandValidator : ICommandValidator<ValidatedCommand>
    {
        public ErrorList IsValid(ValidatedCommand command)
        {
            var errors = new ErrorList();

            if (!command.IAmValid)
            {
                errors.Add("Command is not valid!");
            }
            return errors;
        }
    }

    public class ValidatedCommandHandler : ICommandHandler<ValidatedCommand>
    {
        public void Handle(ValidatedCommand command)
        {
            Console.WriteLine($"Validated command is always valid!");
        }
    }
}
