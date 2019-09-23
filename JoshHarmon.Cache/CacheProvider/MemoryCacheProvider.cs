using System;
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
        private readonly IDictionary<string, (object Item, DateTime TimeStamp)> _cache;
        private readonly object _lock = new object();

        private DateTime Now() => _config.UseUtc ? DateTime.UtcNow : DateTime.Now;

        public MemoryCacheProvider(ICacheConfig config) : this(config, new Dictionary<string, (object Item, DateTime ModifiedAt)>())
        { }

        public MemoryCacheProvider(ICacheConfig config, IDictionary<string, (object Item, DateTime ModifiedAt)> cacheStore)
        {
            Assert.NotNull(config, nameof(config));
            Assert.NotNull(cacheStore, nameof(cacheStore));

            _config = config;
            _cache = cacheStore;
        }

        public async Task<bool> AddAsync<T>(string key, T item) => await Task.FromResult(Add(key, item));

        private bool Add<T>(string key, T item)
        {
            lock (_lock)
            {
                if (_cache.ContainsKey(key))
                {
                    if (IsExpired(key))
                        throw new ArgumentException($"Argument '{nameof(key)}' with value '{key}'" +
                            $"already exists in cache.");
                    _cache.Remove(key);
                }

                _cache.Add(key, (item, Now()));
            }
            return true;
        }

        public async Task ClearAsync() => await Task.Run(Clear);

        private void Clear()
        {
            lock (_lock)
            {
                _cache.Clear();
            }
        }

        public async Task<bool> ContainsKeyAsync(string key) => await Task.FromResult(ContainsKey(key));

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

        public async Task<T> GetAsync<T>(string key) => await Task.FromResult(Get<T>(key));

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

        public async Task<bool> IsEmptyAsync() => await Task.FromResult(IsEmpty());

        private bool IsEmpty() => _cache.Count == 0;

        public async Task<bool> RemoveAsync(string key) => await Task.FromResult(Remove(key));

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

        public async Task<DateTime?> GetExpirationAsync(string key)
            => await Task.FromResult(GetExpiration(key));

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

            return _cache[key].TimeStamp + _config.DefaultExpirationDuration;
        }

        private bool IsExpired(string key)
            => Now() - _cache[key].TimeStamp > _config.DefaultExpirationDuration;
    }
}
