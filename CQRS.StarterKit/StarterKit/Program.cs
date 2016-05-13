using System;
using System.Diagnostics;
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


            Sample1(); // basic command

            Console.WriteLine();
            Console.WriteLine("****************************");
            Console.WriteLine();

            Sample2(); // validated command

            Console.WriteLine();
            Console.WriteLine("****************************");
            Console.WriteLine();

            Sample3(); // validated command that reaches into database and does validation. Also transaction is applied

            Console.WriteLine();
            Console.WriteLine("****************************");
            Console.WriteLine();

            Sample4();

            Console.WriteLine();
            Console.WriteLine("****************************");
            Console.WriteLine();

            Sample5();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Execution complete. Press any key.");
            Console.ReadKey();
        }


        static void Sample1()
        {
            var basicCommand = new BasicCommand("Hello World");

            mediator.ProcessCommand(basicCommand);
        }


        static void Sample2()
        {
            var errors = mediator.ProcessCommand(new ValidatedCommand(true));

            if (!errors.IsValid())
            {
                Console.WriteLine($"Validation failed. Errors are: {errors}");
            }

            errors = mediator.ProcessCommand(new ValidatedCommand(false));
            if (!errors.IsValid())
            {
                Console.WriteLine($"Validation failed. Errors are: {errors}");
            }
        }


        static void Sample3()
        {
            var errors = mediator.ProcessCommand(new CreateProductCommand()
                {
                    ProductId = Guid.NewGuid(), // assign this on the client so you can use this later for redirection
                    ProductName = "Carrot Cake",
                    ProductDescription = "Juicy cake made of carrots",
                });
            if (!errors.IsValid())
            {
                Console.WriteLine($"Validation failed. Errors are: {errors}");
            }


            errors = mediator.ProcessCommand(new CreateProductCommand()
            {
                ProductId = Guid.NewGuid(), // assign this on the client so you can use this later for redirection
                ProductName = "Carrot Cake",
                ProductDescription = "Lets make a nice cake that is orange!",
            });
            if (!errors.IsValid())
            {
                Console.WriteLine($"Validation failed. Errors are: {errors}");
            }
        }

        static void Sample4()
        {
            var query = new GetProductsQuery(includeInactive: false);
            var products = mediator.Request(query);

            foreach (var product in products)
            {
                Console.WriteLine($"Product Name is : {product.Name}");
            }
        }

        static void Sample5()
        {
            var query = new GetAllProductsQuery();
            var result = mediator.Request(query); // first execution => very slow

            result = mediator.Request(query); // second execution - results are taken from cache
        }
    }
}
