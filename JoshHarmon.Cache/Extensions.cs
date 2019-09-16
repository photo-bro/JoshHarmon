using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoshHarmon.Cache.CacheProvider.Interface;

namespace JoshHarmon.Cache
{
    public static class Extensions
    {
        public static async Task<T> TryGetFromCacheAsync<T>(this ICacheProvider cacheProvider, string key,
            Func<Task<T>> retrievalFunc)
        {
            if (await cacheProvider.ContainsKeyAsync(key))
            {
                return await cacheProvider.GetAsync<T>(key);
            }

            var data = await retrievalFunc();
            _ = cacheProvider.AddAsync(key, data);
            return data;
        }

        public static async Task<IEnumerable<T>> TryGetEnumerableFromCacheAsync<T>(this ICacheProvider cacheProvider,
            string key, Func<Task<IEnumerable<T>>> retrievalFunc)
        {
            if (await cacheProvider.ContainsKeyAsync(key))
            {
                return await cacheProvider.GetAsync<T[]>(key);
            }

            var data = await retrievalFunc();
            _ = cacheProvider.AddAsync(key, data.ToArray());
            return data;
        }
    }
}
