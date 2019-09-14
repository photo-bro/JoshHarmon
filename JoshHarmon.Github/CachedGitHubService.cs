using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.Github.Interface;
using JoshHarmon.Github.Models;
using Octokit;
using Octokit.Reactive;

namespace JoshHarmon.Github
{
    public class CachedGithubService : IGithubService, ICached
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IGithubConfig _config;
        private IObservableGitHubClient _client;

        public CachedGithubService(ICacheProvider cacheProvider, IGithubConfig config)
        {
            _cacheProvider = cacheProvider;
            _config = config;
            try
            {
                _client = new ObservableGitHubClient(new ProductHeaderValue(_config.UserName));
            }
            catch (Exception e)
            {
                throw new Exception($"Error creating GitHub client for user '{_config.UserName}'", e);
            }

            if (_client == null)
            {
                throw new ArgumentException($"Github user '{_config.UserName}' not found or invalid");
            }
        }

        public Task<DateTime?> GetKeyExpirationAsync(string key) => throw new NotImplementedException();

        public async Task<EventInfo> GetRepositoryEvents(string repositoryName)
        {
            var repo = await _client.Repository.GetAllPublic().SingleOrDefaultAsync(r => r.Name == repositoryName);
            if (repo == null)
            {
                throw new ArgumentException($"Repository name '{repositoryName}' does not exist for user " +
                    $"'{await _client.User.Current()}'");
            }

            //var events = repo.

            return null;
        }

        public Task<RepoStats> GetRepositoryStats(string repositoryName) => throw new NotImplementedException();

        public Task FlushAsync() => throw new NotImplementedException();

        public Task PurgeKeyAsync(string key) => throw new NotImplementedException();
    }
}
