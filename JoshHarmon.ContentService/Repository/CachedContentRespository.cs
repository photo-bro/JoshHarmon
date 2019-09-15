﻿using System;
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
            Assert.NotNull(contentRepository);
            Assert.NotNull(cacheProvider);

            _contentRepository = contentRepository;
            _cacheProvider = cacheProvider;
        }

        public Task FlushAsync() => _cacheProvider.ClearAsync();

        public Task<DateTime?> GetKeyExpirationAsync(string key) => _cacheProvider.GetExpirationAsync(key);

        public Task PurgeKeyAsync(string key) => _cacheProvider.RemoveAsync(key);

        public async Task<IEnumerable<ConnectModel>> ReadAllConnectModels()
            => await _cacheProvider.TryGetFromCacheAsync(ConnectModelsKey, _contentRepository.ReadAllConnectModels);

        public async Task<IEnumerable<PanelModel>> ReadAllPanelModels()
            => await _cacheProvider.TryGetFromCacheAsync(PanelModelsKey, _contentRepository.ReadAllPanelModels);

        public async Task<IEnumerable<ProjectModel>> ReadAllProjectModels()
            => await _cacheProvider.TryGetFromCacheAsync(ProjectModelsKey, _contentRepository.ReadAllProjectModels);
    }
}