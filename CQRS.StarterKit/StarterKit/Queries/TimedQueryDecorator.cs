using System.Diagnostics;
using StarterKit.Logging;

namespace StarterKit.Queries
{
    public class TimedQueryDecorator<TQuery, TResult> :
        IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> decorated;
        private readonly ILoggingService logger;

        public TimedQueryDecorator(IQueryHandler<TQuery, TResult> decorated, ILoggingService logger)
        {
            this.decorated = decorated;
            this.logger = logger;
            this.logger.SetLoggerName("Timed Query");
        }

        public TResult Handle(TQuery query)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = decorated.Handle(query);
            stopwatch.Stop();

            //TODO possibly only log time only when amount of time exceeds a certain amount of time
            // something for performance optimisation

            logger.Info("TIMED: Querry {0} executed in {1}ms", query.GetType().Name, stopwatch.ElapsedMilliseconds);
            return result;
        }
    }
}
