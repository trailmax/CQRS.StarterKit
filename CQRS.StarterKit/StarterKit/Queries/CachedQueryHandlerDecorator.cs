namespace StarterKit.Queries
{
    /// <summary>
    /// Decorator around query handling to add caching into pipeline.
    /// 
    /// Before executing query, this decorator checks if query implements ICachedQuery.
    /// If query is not marked as cached, nothing extra happens and controll is passed to the wrapped handler.
    /// If query is ICachedQuery, before executing the query, this wrapper checks that cache does not yet contain results of the execution.
    /// If cache already contains the results - return cached results without executing of the query.
    /// Otherwise execute the query and put the result into cache for future executions.
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class CachedQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public IQueryHandler<TQuery, TResult> Decorated { get; set; }
        private readonly ICacheProvider cacheProvider;

        public CachedQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated, ICacheProvider cacheProvider)
        {
            Decorated = decorated;
            this.cacheProvider = cacheProvider;
        }


        public TResult Handle(TQuery query)
        {
            var cachedQuery = query as ICachedQuery;

            if (cachedQuery == null)
            {
                return Decorated.Handle(query);
            }

            var cacheKey = cachedQuery.CacheKey;
            var cachedObject = cacheProvider.Get(cacheKey);

            if (cachedObject != null && cachedObject is TResult)
            {
                return (TResult)cachedObject;
            }
            
            var cachedResult = Decorated.Handle(query);

            cacheProvider.Set(cachedQuery.CacheKey, cachedResult, cachedQuery.CacheDuration);
            return cachedResult;
        }
    }
}