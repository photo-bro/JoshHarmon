using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.ContentService.Models;

namespace JoshHarmon.ContentService.Repository
{
    public class CachedJsonFileContentRespository : JsonFileContentRespository, ICached
    {
        public static string PanelModelsKey = "panel-models";
        public static string ConnectModelsKey = "connect-models";
        public static string ProjectModelsKey = "project-models";

        private readonly ICacheProvider _cacheProvider;

        public CachedJsonFileContentRespository(ICacheProvider cacheProvider, string fileName)
            : base(fileName)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task FlushAsync() => await _cacheProvider.ClearAsync();

        public async Task<DateTime?> GetKeyExpirationAsync(string key)
            => await _cacheProvider.GetExpirationAsync(key);

        public async Task PurgeKeyAsync(string key) => await _cacheProvider.RemoveAsync(key);

        public override async Task<IEnumerable<ConnectModel>> ReadAllConnectModels()
            => await GetCollectionFromCache(ConnectModelsKey, base.ReadAllConnectModels);

        public override async Task<IEnumerable<PanelModel>> ReadAllPanelModels()
            => await GetCollectionFromCache(PanelModelsKey, base.ReadAllPanelModels);

        public override async Task<IEnumerable<ProjectModel>> ReadAllProjectModels()
            => await GetCollectionFromCache(ProjectModelsKey, base.ReadAllProjectModels);

        private async Task<IEnumerable<T>> GetCollectionFromCache<T>(string key, Func<Task<IEnumerable<T>>> fallbackRetrievalFunc)
        {
            if (await _cacheProvider.ContainsKeyAsync(key))
                return await _cacheProvider.GetAsync<T[]>(key);

            var models = await fallbackRetrievalFunc();
            _ = _cacheProvider.AddAsync(key, models.ToArray());
            return models;
        }
    }
}
