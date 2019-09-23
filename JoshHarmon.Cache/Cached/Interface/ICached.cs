using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoshHarmon.Cache.Cached.Interface
{
    public interface ICached
    {
        Task FlushAsync();
        Task PurgeKeyAsync(string key);
        Task<DateTime?> GetKeyExpirationAsync(string key);
        Task<IEnumerable<(string Key, DateTime Expiration)>> GetAllKeysAsync();
    }
}
