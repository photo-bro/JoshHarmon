using System;
using System.Threading.Tasks;

namespace JoshHarmon.Cache.Interface
{
    public interface ICacheProvider
    {
        Task<DateTime?> GetExpirationAsync(string key);
        Task<T> GetAsync<T>(string key);
        Task<bool> AddAsync<T>(string key, T item);
        Task<bool> RemoveAsync(string key);
        Task ClearAsync();
        Task<bool> IsEmptyAsync();
        Task<bool> ContainsKeyAsync(string key);
    }
}
