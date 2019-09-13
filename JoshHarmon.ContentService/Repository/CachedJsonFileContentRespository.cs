using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.ContentService.Models;
using Microsoft.Extensions.Logging;

namespace JoshHarmon.ContentService.Repository
{
    public class CachedJsonFileContentRespository : JsonFileContentRespository, ICached
    {
        public static string PanelModelsKey = "panel-models";
        public static string ConnectModelsKey = "projects-models";
        public static string ProjectModelsKey = "connect-models";

        private readonly ICacheProvider _cacheProvider;

        public CachedJsonFileContentRespository(ICacheProvider cacheProvider,
            string fileName,
            ILogger<JsonFileContentRespository> baseLogger)
            : base(fileName, baseLogger)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task FlushAsync()
        {
            await _cacheProvider.ClearAsync();
        }

        public async Task<DateTime?> GetKeyExpirationAsync(string key)
            => await _cacheProvider.GetExpirationAsync(key);

        public async Task PurgeKeyAsync(string key) => await _cacheProvider.RemoveAsync(key);

        public override async Task<IEnumerable<ConnectModel>> ReadAllConnectModels()
        {
            if (await _cacheProvider.ContainsKeyAsync(ConnectModelsKey))
                return await _cacheProvider.GetAsync<ConnectModel[]>(ConnectModelsKey);

            var models = await base.ReadAllConnectModels();
            _ = _cacheProvider.AddAsync(ConnectModelsKey, models);
            return models;
        }

        public override async Task<IEnumerable<PanelModel>> ReadAllPanelModels()
        {
            if (await _cacheProvider.ContainsKeyAsync(PanelModelsKey))
                return await _cacheProvider.GetAsync<PanelModel[]>(PanelModelsKey);

            var models = await base.ReadAllPanelModels();
            _ = _cacheProvider.AddAsync(PanelModelsKey, models);
            return models;
        }

        public override async Task<IEnumerable<ProjectModel>> ReadAllProjectModels()
        {
            if (await _cacheProvider.ContainsKeyAsync(ProjectModelsKey))
                return await _cacheProvider.GetAsync<ProjectModel[]>(ProjectModelsKey);

            var models = await base.ReadAllProjectModels();
            _ = _cacheProvider.AddAsync(ProjectModelsKey, models);
            return models;
        }
    }
}
