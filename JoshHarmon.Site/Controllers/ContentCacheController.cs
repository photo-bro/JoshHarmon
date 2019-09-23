﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            Assert.NotNull(contentCaches, nameof(contentCaches));
            Assert.NotNull(logger, nameof(logger));

            _contentCaches = contentCaches;
            _logger = logger;
        }

        [LocalHost]
        [HttpDelete("/cache")]
        public IActionResult PurgeCache()
        {
            try
            {
                var t = Task.WhenAll(_contentCaches.Select(async c => await c.FlushAsync()));
                t.Start();
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
            CacheInfo[] cacheInfos;
            try
            {
                var tasks = _contentCaches.Select(async c => (CacheName: c.GetType().ToString(), Expiration: await c.GetKeyExpirationAsync(key)));

                cacheInfos = (await Task.WhenAll(tasks))
                    .Where(ci => ci.Expiration != null)
                    .Select(ci => new CacheInfo
                    {
                        Name = ci.CacheName,
                        Items = new[] {
                            new CacheItemExpiration {
                                ItemKey = key,
                                Expiration = ci.Expiration.Value
                            }
                        }
                    })
                    .ToArray();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to purge {Key} from content cache", key);
                return new ObjectResult($"Error retrieving expiration for key '{key}' from content cache")
                { StatusCode = 500 };
            }

            if (cacheInfos == null || cacheInfos.Length == 0)
            {
                return NotFound($"Item with key '{key}' not found in cache");
            }

            return Ok(new { Caches = cacheInfos });
        }

        [LocalHost]
        [HttpGet("/cache/")]
        public async Task<IActionResult> GetCacheItems()
        {
            var keyTasks = _contentCaches.Select(async c => (CacheName: c.GetType().ToString(), Keys: await c.GetAllKeysAsync()));

            var keys = await Task.WhenAll(keyTasks);

            var cacheInfos = keys.Select(c => new CacheInfo
            {
                Name = c.CacheName,
                Items = c.Keys.Select(k => new CacheItemExpiration
                {
                    ItemKey = k.Key,
                    Expiration = k.Expiration
                }).ToArray()
            })
                .ToArray();

            return Ok(new { Caches = cacheInfos });
        }
    }

    internal class CacheInfo
    {
        public string Name { get; set; }
        public CacheItemExpiration[] Items { get; set; }
    }

    internal class CacheItemExpiration
    {
        public string ItemKey { get; set; }
        public DateTime Expiration { get; set; }
    }
}
