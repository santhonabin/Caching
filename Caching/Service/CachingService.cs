using Caching.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Caching.Service
{
    public class CachingService<T> : ICachingService<T> where T : class
    {
        private IMemoryCache _cache;

        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<T> GetCache(object cacheId)
        {
            _cache.TryGetValue(cacheId, out IEnumerable<T> cacheValue);
            return cacheValue;
        }

        public void SetCache(object cacheId, IEnumerable<T> cacheValue)
        {
            _cache.Set(cacheId,cacheValue);
        }

        public void SetCacheWithExpiration(object cacheId, IEnumerable<T> cacheValue) => _cache.Set(cacheId, cacheValue, new MemoryCacheEntryOptions
        {
            //todo: Add the absolute expire Timespan to appsetting.json
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30),
            //todo: Add the sliding expire Timespan to appsetting.json
            SlidingExpiration = TimeSpan.FromDays(7)
        });

        public void SetCacheWithSlidingExpiration(object cacheId, IEnumerable<T> cacheValue) => _cache.Set(cacheId, cacheValue, new MemoryCacheEntryOptions
        {
            //todo: Add the sliding expire Timespan to appsetting.json
            SlidingExpiration = TimeSpan.FromDays(7)
        });

        public void Remove(object key)
        {
            _cache.Remove(key);
        }
    }
}
