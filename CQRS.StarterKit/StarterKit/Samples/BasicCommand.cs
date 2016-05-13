using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
