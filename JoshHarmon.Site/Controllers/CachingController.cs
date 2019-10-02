using System;
using System.Linq;
using System.Threading.Tasks;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.Shared;
using JoshHarmon.Site.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JoshHarmon.Site.Controllers
{
    public class CachingController : Controller
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly ILogger<CachingController> _logger;

        public CachingController(ICacheProvider cacheProvider, ILogger<CachingController> logger)
        {
            Assert.NotNull(cacheProvider, nameof(cacheProvider));
            Assert.NotNull(logger, nameof(logger));

            _cacheProvider = cacheProvider;
            _logger = logger;
        }

        [LocalHost]
        [HttpDelete("/cache")]
        public IActionResult PurgeCache()
        {
            try
            {
                _cacheProvider.ClearAsync().Start();
                _logger.LogInformation("Content cache purged at {LocalTime}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to purge content cache");
                return new ObjectResult("Error purging content cache")
                { StatusCode = 500 };
            }

            return Accepted("Cache purged");
        }

        [LocalHost]
        [HttpDelete("/cache/{key}")]
        public async Task<IActionResult> PurgeCacheItem(string key)
        {
            try
            {
                var tasks = _cacheProvider.RemoveAsync(key);
                await Task.WhenAll(tasks);
                _logger.LogInformation("Key {Key} purged from content cache at {LocalTime}", key, DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to purge {Key} from content cache", key);
                return new ObjectResult($"Error purging key '{key}' content cache")
                { StatusCode = 500 };
            }

            return Ok($"Item with key '{key}' purged");
        }

        [HttpGet("/cache/{key}")]
        public async Task<IActionResult> GetItemExpiration(string key)
        {
            var cacheInfo = (CacheName: _cacheProvider.GetType().ToString(),
                            Expiration: await _cacheProvider.GetExpirationAsync(key));

            if (cacheInfo == default)
            {
                return NotFound($"Item with key '{key}' not found in cache");
            }

            return Ok(new { Caches = cacheInfo });
        }

        [LocalHost]
        [HttpGet("/cache/")]
        public async Task<IActionResult> GetCacheItems()
        {
            var (CacheName, Keys) = (_cacheProvider.GetType().ToString(), await _cacheProvider.GetAllKeysAsync());

            var cacheInfo = new CacheInfo(
                name: CacheName,
                items: Keys.Select(k => new CacheItemExpiration(
                     itemKey: k.Key,
                     expiration: k.Expiration
                 )).ToArray());

            return Ok(new { Caches = cacheInfo });
        }
    }

    internal class CacheInfo
    {
        public string Name { get; }
        public CacheItemExpiration[] Items { get; }

        public CacheInfo(string name, CacheItemExpiration[] items)
        {
            Name = name;
            Items = items;
        }
    }

    internal class CacheItemExpiration
    {
        public string ItemKey { get; }
        public DateTime Expiration { get; }

        public CacheItemExpiration(string itemKey, DateTime expiration)
        {
            ItemKey = itemKey;
            Expiration = expiration;
        }
    }
}
