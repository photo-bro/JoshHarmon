using System;
using System.Threading.Tasks;

namespace JoshHarmon.Cache.Cached.Interface
{
    public interface ICached
    {
        Task FlushAsync();
        Task PurgeKeyAsync(string key);
        Task<DateTime?> GetKeyExpirationAsync(string key);
    }
}
