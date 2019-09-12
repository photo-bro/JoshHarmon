using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.Cache.Interface;
using JoshHarmon.ContentService.Models;
using Microsoft.Extensions.Logging;

namespace JoshHarmon.ContentService.Repository
{
    public class CachedJsonFileContentRespository : JsonFileContentRespository, ICached
    {
        public static string PanelModelsKey = "connect-models";
        public static string ConnectModelsKey = "projects-models";
        public static string ProjectModelsKey = "connect-models";

        private readonly ICacheProvider _cacheProvider;
        
        public CachedJsonFileContentRespository(string fileName,
            ILogger<JsonFileContentRespository> baseLogger,
            ICacheProvider cacheProvider)
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
                return await _cacheProvider.GetAsync<IEnumerable<ConnectModel>>(ConnectModelsKey);

            return await base.ReadAllConnectModels();
        }

        public override async Task<IEnumerable<PanelModel>> ReadAllPanelModels()
        {
            if (await _cacheProvider.ContainsKeyAsync(ConnectModelsKey))
                return await _cacheProvider.GetAsync<IEnumerable<PanelModel>>(PanelModelsKey);

            return await base.ReadAllPanelModels();
        }

        public override async Task<IEnumerable<ProjectModel>> ReadAllProjectModels()
        {
            if (await _cacheProvider.ContainsKeyAsync(ConnectModelsKey))
                return await _cacheProvider.GetAsync<IEnumerable<ProjectModel>>(ProjectModelsKey);

            return await base.ReadAllProjectModels();
        }
    }
}
