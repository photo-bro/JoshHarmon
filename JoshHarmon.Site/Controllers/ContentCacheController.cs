using System;
using System.Threading.Tasks;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Site.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JoshHarmon.Site.Controllers
{
    public class ContentCacheController : Controller
    {
        private readonly ICached _contentCache;
        private readonly ILogger<ContentCacheController> _logger;

        public ContentCacheController(ICached contentCache, ILogger<ContentCacheController> logger)
        {
            _contentCache = contentCache;
            _logger = logger;
        }

        [LocalHost]
        [HttpDelete("/cache/purge")]
        public async Task<IActionResult> PurgeCache()
        {
            try
            {
                await _contentCache.FlushAsync();
                _logger.LogInformation("Content cache purged at {LocalTime}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to purge content cache");
                return new ObjectResult("Error purging content cache")
                { StatusCode = 500 };
            }

            return Ok("Cache purged");
        }

        [LocalHost]
        [HttpDelete("/cache/purge/{key}")]
        public async Task<IActionResult> PurgeCacheItem(string key)
        {
            try
            {
                await _contentCache.PurgeKeyAsync(key);
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
            DateTime? expiration;
            try
            {
                expiration = await _contentCache.GetKeyExpirationAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to purge {Key} from content cache", key);
                return new ObjectResult($"Error retrieving expiration for key '{key}' from content cache")
                { StatusCode = 500 };
            }

            return Ok(expiration?.ToString("O") ?? "Item not found");
        }
    }
}
