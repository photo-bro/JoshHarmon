using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.Cache;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.ContentService.Models;
using JoshHarmon.ContentService.Repository.Interface;
using JoshHarmon.Shared;

namespace JoshHarmon.ContentService.Repository
{
    public class CachedContentRepository : ICachedContentRepository
    {
        public static string PanelModelsKey = "panel-models";
        public static string ConnectModelsKey = "connect-models";
        public static string ProjectModelsKey = "project-models";

        private readonly IContentRepository _contentRepository;
        private readonly ICacheProvider _cacheProvider;

        public CachedContentRepository(IContentRepository contentRepository,
            ICacheProvider cacheProvider)
        {
            Assert.NotNull(contentRepository, nameof(contentRepository));
            Assert.NotNull(cacheProvider, nameof(cacheProvider));

            _contentRepository = contentRepository;
            _cacheProvider = cacheProvider;
        }

        public Task FlushAsync() => _cacheProvider.ClearAsync();

        public Task<DateTime?> GetKeyExpirationAsync(string key) => _cacheProvider.GetExpirationAsync(key);

        public Task PurgeKeyAsync(string key) => _cacheProvider.RemoveAsync(key);

        public Task<IEnumerable<(string Key, DateTime Expiration)>> GetAllKeysAsync()
             => _cacheProvider.GetAllKeysAsync();

        public async Task<IEnumerable<ConnectModel>> ReadAllConnectModels()
            => await _cacheProvider.TryGetEnumerableFromCacheAsync(ConnectModelsKey, _contentRepository.ReadAllConnectModels);

        public async Task<IEnumerable<PanelModel>> ReadAllPanelModels()
            => await _cacheProvider.TryGetEnumerableFromCacheAsync(PanelModelsKey, _contentRepository.ReadAllPanelModels);

        public async Task<IEnumerable<ProjectModel>> ReadAllProjectModels()
            => await _cacheProvider.TryGetEnumerableFromCacheAsync(ProjectModelsKey, _contentRepository.ReadAllProjectModels);
    }
}
