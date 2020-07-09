using System.Collections.Generic;

namespace Caching.Interface
{
    public interface ICachingService<T> where T : class
    {
        /// <summary>
        /// Get the cache value
        /// </summary>
        /// <param name="cacheId"></param>
        /// <returns></returns>
        IEnumerable<T> GetCache(object cacheId);

        /// <summary>
        /// Set the cache value
        /// </summary>
        /// <param name="cacheId"></param>
        /// <param name="cacheValue"></param>
        void SetCache(object cacheId, IEnumerable<T> cacheValue);

        /// <summary>
        /// Set the cache value with two types of Expiration : 
        /// 1. Absolute expiration using TimeSpan ie., After certain period, the cache will expire
        /// 2. Sliding expiration ie., evict if not accessed for particular number of days
        /// </summary>
        /// <param name="cacheId"></param>
        /// <param name="cacheValue"></param>
        void SetCacheWithExpiration(object cacheId, IEnumerable<T> cacheValue);

        /// <summary>
        /// Set the cache value with Sliding expiration ie., evict if not accessed for particular number of days
        /// </summary>
        /// <param name="cacheId"></param>
        /// <param name="cacheValue"></param>
        void SetCacheWithSlidingExpiration(object cacheId, IEnumerable<T> cacheValue);

        /// <summary>
        /// Clear Cache based on the Key
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);
    }
}
