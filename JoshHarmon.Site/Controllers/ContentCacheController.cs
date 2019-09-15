using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Shared;
using JoshHarmon.Site.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JoshHarmon.Site.Controllers
{
    public class ContentCacheController : Controller
    {
        private readonly IEnumerable<ICached> _contentCaches;
        private readonly ILogger<ContentCacheController> _logger;

        public ContentCacheController(IEnumerable<ICached> contentCaches, ILogger<ContentCacheController> logger)
        {
            Assert.NotNull(contentCaches);
            Assert.NotNull(logger);

            _contentCaches = contentCaches;
            _logger = logger;
        }

        [LocalHost]
        [HttpDelete("/cache/purge")]
        public async Task<IActionResult> PurgeCache()
        {
            try
            {
                var tasks = _contentCaches.Select(c => c.FlushAsync());
                await Task.WhenAll(tasks);
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
                var tasks = _contentCaches.Select(c => c.PurgeKeyAsync(key));
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
            DateTime?[] expirations;
            try
            {
                var expirationTasks = _contentCaches.Select(c => c.GetKeyExpirationAsync(key));
                expirations = (await Task.WhenAll(expirationTasks)).Where(e => e != null).ToArray();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to purge {Key} from content cache", key);
                return new ObjectResult($"Error retrieving expiration for key '{key}' from content cache")
                { StatusCode = 500 };
            }

            var output = string.Join("\n",
                expirations?
                .Select(e => e?.ToString("O") ?? "")
                .Where(s => !string.IsNullOrEmpty(s)));
            output = string.IsNullOrEmpty(output) ? "Item not found" : output;

            return Ok(output);
        }
    }
}
