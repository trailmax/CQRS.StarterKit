using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using StarterKit.Samples;

namespace StarterKit
{
    class Program
    {
        private static IMediator mediator;
        private static Container container;

        static void Main(string[] args)
        {
            container = InjectorConfig.Configure();
            mediator = container.GetInstance<IMediator>();


            Sample1();

            Console.ReadKey();
        }


        static void Sample1()
        {
            var basicCommand = new BasicCommand("Hello World");

            mediator.ProcessCommand(basicCommand);
        }
    }
}
