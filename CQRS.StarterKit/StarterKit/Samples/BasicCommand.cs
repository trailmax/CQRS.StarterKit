using System;
using StarterKit.Commands;

namespace StarterKit.Samples
{
    public class BasicCommand : ICommand
    {
        public BasicCommand(string sayWhat)
        {
            SayWhat = sayWhat;
        }

        public String SayWhat { get; private set; }
    }

    public class BasicCommandHandler : ICommandHandler<BasicCommand>
    {
        public void Handle(BasicCommand command)
        {
            Console.WriteLine(command.SayWhat);
        }
    }
}
