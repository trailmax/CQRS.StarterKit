using System;
using System.Collections.Generic;

namespace StarterKit.Queries
{
    public interface ICacheProvider
    {
        object Get(string key);
        TResult Get<TResult>(string key) where TResult : class;
        void Set(string key, object data, TimeSpan cacheTime);
        bool IsSet(string key);
        void Invalidate(string key);
        void InvalidateAll();
    }


    /// <summary>
    /// Basic implementation of ICachedProvider that uses a dictionary as a cache.
    /// Only useful for samples and learning. Not recommended for production use.
    /// 
    /// .Net already contains a few caching implementations. Look up the best one for your environment
    /// </summary>
    public class DictionaryCacheProvider : ICacheProvider
    {
        private Dictionary<String, Object> cachedObjects;

        public DictionaryCacheProvider()
        {
            this.cachedObjects = new Dictionary<string, object>();
        }

        public object Get(string cacheKey)
        {
            object result;
            if (cachedObjects.TryGetValue(cacheKey, out result))
            {
                return result;
            }
            return null;
        }

        public void Set(string cacheKey, object cachedResult, TimeSpan cacheDuration)
        {
            // for simplicity of the sample ignore cache duration 
            cachedObjects[cacheKey] = cachedResult;
        }


        public TResult Get<TResult>(string key) where TResult : class
        {
            return Get(key) as TResult;
        }


        public bool IsSet(string key)
        {
            return cachedObjects.ContainsKey(key);
        }


        public void Invalidate(string key)
        {
            cachedObjects[key] = null;
        }


        public void InvalidateAll()
        {
            cachedObjects = new Dictionary<string, object>();
        }
    }
}