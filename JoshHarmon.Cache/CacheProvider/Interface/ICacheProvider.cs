using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoshHarmon.Cache.CacheProvider.Interface
{
    public interface ICacheProvider
    {
        Task<DateTime?> GetExpirationAsync(string key);
        Task<IEnumerable<(string Key, DateTime Expiration)>> GetAllKeysAsync();
        Task<T> GetAsync<T>(string key);
        Task<bool> AddAsync<T>(string key, T item);
        Task<bool> RemoveAsync(string key);
        Task ClearAsync();
        Task<bool> IsEmptyAsync();
        Task<bool> ContainsKeyAsync(string key);
    }
}
