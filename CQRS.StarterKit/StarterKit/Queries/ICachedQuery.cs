using System;

namespace StarterKit.Queries
{
    /// <summary>
    /// Marker to say that Query is cached and CachedQueryDecorator will handle this query appropriately
    /// </summary>
    public interface ICachedQuery
    {
        String CacheKey { get; }
        TimeSpan CacheDuration { get; }
    }
}