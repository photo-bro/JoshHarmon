using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.Cache.Interface;
using JoshHarmon.Shared;

namespace JoshHarmon.Cache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private readonly ICacheConfig _config;
        private readonly IDictionary<string, (object Item, DateTime ExpiresAt)> _cache;
        private readonly object _lock = new object();

        private DateTime Now() => _config.UseUtc ? DateTime.UtcNow : DateTime.Now;

        private DateTime KeyExpireTime => Now() + _config.DefaultExpirationDuration;

        public MemoryCacheProvider(ICacheConfig config)
            : this(config, new Dictionary<string, (object Item, DateTime ExpiresAt)>())
        { }

        public MemoryCacheProvider(ICacheConfig config, IDictionary<string, (object Item, DateTime ExpiresAt)> cacheStore)
        {
            Assert.NotNull(config, nameof(config));
            Assert.NotNull(cacheStore, nameof(cacheStore));

            _config = config;
            _cache = cacheStore;
        }

        public Task<bool> AddAsync<T>(string key, T item) => Task.FromResult(Add(key, item));

        private bool Add<T>(string key, T item)
        {
            if (item == default)
            {
                throw new ArgumentNullException($"'{nameof(item)}' cannot be null or default");
            }

            lock (_lock)
            {
                if (_cache.ContainsKey(key))
                {
                    if (IsExpired(key))
                        throw new ArgumentException($"Argument '{nameof(key)}' with value '{key}'" +
                            $"already exists in cache.");
                    _cache.Remove(key);
                }

                _cache.Add(key, (item, KeyExpireTime));
            }
            return true;
        }

        public Task ClearAsync() => Task.Run(Clear);

        private void Clear()
        {
            lock (_lock)
            {
                _cache.Clear();
            }
        }

        public Task<bool> ContainsKeyAsync(string key) => Task.FromResult(ContainsKey(key));

        private bool ContainsKey(string key)
        {
            lock (_lock)
            {
                if (_cache.ContainsKey(key))
                {
                    if (IsExpired(key))
                    {
                        _cache.Remove(key);
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public Task<T> GetAsync<T>(string key) => Task.FromResult(Get<T>(key));

        public T Get<T>(string key)
        {
            object item;
            lock (_lock)
            {
                if (_cache.ContainsKey(key))
                {
                    if (IsExpired(key))
                        throw new ArgumentException($"Argument '{nameof(key)}' with value '{key}' does " +
                            $"not exist in cache.");

                    item = _cache[key].Item;
                }
                else
                    throw new ArgumentException($"Argument '{nameof(key)}' with value '{key}' does " +
                            $"not exist in cache.");
            }

            if (item.GetType() != typeof(T))
                throw new ArgumentException($"Argument '{nameof(T)}' with value '{typeof(T)}' does " +
                    $"not match expected type of '{item.GetType()}' for item.");

            return (T)item;
        }

        public Task<bool> IsEmptyAsync() => Task.FromResult(IsEmpty());

        private bool IsEmpty() => _cache.Count == 0;

        public Task<bool> RemoveAsync(string key) => Task.FromResult(Remove(key));

        private bool Remove(string key)
        {
            lock (_lock)
            {
                if (_cache.ContainsKey(key))
                {
                    _cache.Remove(key);
                }
            }
            return true;
        }

        public Task<DateTime?> GetExpirationAsync(string key) => Task.FromResult(GetExpiration(key));

        private DateTime? GetExpiration(string key)
        {
            if (!_cache.ContainsKey(key))
            {
                return null;
            }

            if (IsExpired(key))
            {
                _cache.Remove(key);
                return null;
            }

            return _cache[key].ExpiresAt;
        }

        public Task<IEnumerable<(string Key, DateTime Expiration)>> GetAllKeysAsync()
            => Task.FromResult(GetAllKeys());

        private IEnumerable<(string Key, DateTime Expiration)> GetAllKeys()
        {
            PruneCache();
            return _cache.Select(kv => (kv.Key, Expiration: kv.Value.ExpiresAt));
        }

        private void PruneCache()
        {
            var cache = _cache.ToList();
            foreach (var item in cache.Where(i => IsExpired(i.Key)))
            {
                _cache.Remove(item.Key);
            }
        }

        private bool IsExpired(string key) => _cache[key].ExpiresAt < Now();
    }
}
