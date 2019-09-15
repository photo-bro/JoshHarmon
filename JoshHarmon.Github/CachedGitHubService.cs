using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JoshHarmon.Cache;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.Github.Interface;
using JoshHarmon.Github.Models;
using JoshHarmon.Shared;
using Octokit;
using Octokit.Reactive;
using Commit = JoshHarmon.Github.Models.Commit;

namespace JoshHarmon.Github
{
    public class CachedGithubService : IGithubService, ICached
    {
        private const string KeyPrefix = "github";
        private readonly ICacheProvider _cacheProvider;
        private readonly IGithubConfig _config;
        private IObservableGitHubClient _client;

        public CachedGithubService(ICacheProvider cacheProvider, IGithubConfig config)
        {
            Assert.NotNull(cacheProvider);
            Assert.NotNull(config);

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

        public async Task<IEnumerable<Commit>> GetRepositoryCommitsAsync(string repositoryName)
             => await _cacheProvider.TryGetFromCacheAsync(
                 key: GetFormattedCacheKey(repositoryName, "repo-commits"),
                 retrievalFunc: () => GetRepositoryCommitsAsyncImpl(repositoryName));

        private async Task<IEnumerable<Commit>> GetRepositoryCommitsAsyncImpl(string repositoryName)
            => await _client
                            .Repository
                            .Commit
                            .GetAll(_config.UserName, repositoryName)
                            .Select(ghc => new Commit
                            {
                                CommitterName = ghc.Commit.Committer.Name,
                                CommitterEmail = ghc.Commit.Committer.Email,
                                DateTime = ghc.Commit.Committer.Date.DateTime,
                                Message = ghc.Commit.Message,
                                Sha = ghc.Sha
                            })
                            .ToList();


        public async Task<RepoStats> GetRepositoryStatsAsync(string repositoryName)
            => await _cacheProvider.TryGetFromCacheAsync(
                key: GetFormattedCacheKey(repositoryName, "repo-stats"),
                retrievalFunc: () => GetRepositoryStatsAsyncImpl(repositoryName));

        private async Task<RepoStats> GetRepositoryStatsAsyncImpl(string repositoryName)
        {
            var repo = await _client.Repository.Get(_config.UserName, repositoryName);
            var stats = await _client.Repository.Statistics.GetContributors(_config.UserName, repositoryName);

            var repoContributors = stats.Select(c => new RepoContributor
            {
                Name = c.Author.Login,
                TotalCommits = c.Weeks.Sum(w => w.C),
                WeeklyAverageAdditions = (int)c.Weeks.Average(w => w.A),
                WeeklyAverageDeletions = (int)c.Weeks.Average(w => w.D),
                WeeklyAverageCommits = (int)c.Weeks.Average(w => w.C),
            }).ToArray();

            return new RepoStats
            {
                CreatedAt = repo.CreatedAt.DateTime,
                LastActivity = repo.UpdatedAt.DateTime,
                Contributors = repoContributors,
                TotalCommits = stats.Sum(c => c.Weeks.Sum(w => w.C))
            };
        }

        public Task FlushAsync() => _cacheProvider.ClearAsync();

        public Task<DateTime?> GetKeyExpirationAsync(string key) => _cacheProvider.GetExpirationAsync(key);

        public Task PurgeKeyAsync(string key) => _cacheProvider.RemoveAsync(key);

        private string GetFormattedCacheKey(string name, string action) => $"{KeyPrefix}_{action}_{name}";
    }
}
