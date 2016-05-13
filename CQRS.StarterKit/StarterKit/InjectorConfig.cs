using System;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using StarterKit.Commands;
using StarterKit.Logging;
using StarterKit.Queries;

namespace StarterKit
{
    public class InjectorConfig
    {
        public static Container Configure()
        {
            var container = new Container();

            container.Register(typeof(IQueryHandler<,>), typeof(Program).Assembly);

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(CachedQueryHandlerDecorator<,>));


            container.Register(typeof(ICommandHandler<>), typeof(Program).Assembly);

            container.Register(typeof(ICommandValidator<>), typeof(Program).Assembly);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(LoggedCommandHandlerDecorator<>));

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactedCommandHandler<>));

            container.RegisterConditional(typeof(ICommandValidator<>), typeof(NullObjectCommandValidator<>), c => !c.Handled);


            container.Register<IMediator, Mediator>();
            container.Register<ICacheProvider, DictionaryCacheProvider>();
            container.Register<ILoggingService, TraceLogger>();

            RegisterTypes(container, nameEndsWith: "Repository");
            RegisterTypes(container, nameEndsWith: "Service");
            return container;
        }


        static void RegisterTypes(Container container, string nameEndsWith)
        {
            var assembly = typeof(Program).Assembly;

            var registrations =
                from type in assembly.GetExportedTypes()
                where type.Name.EndsWith(nameEndsWith, System.StringComparison.OrdinalIgnoreCase)
                where type.GetInterfaces().Any()
                select new { Service = type.GetInterfaces().Single(), Implementation = type };

            foreach (var reg in registrations)
            {
                container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);
            }
        }
    }

    public static class InjectorHelpers
    {
        public static void Register(this Container container, Type type, Assembly asm)
        {
            container.Register(type, new[] { asm });
        }
    }
}
