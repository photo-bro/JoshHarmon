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
            Assert.NotNull(cacheProvider, nameof(cacheProvider));
            Assert.NotNull(config, nameof(config));
            Assert.NotNullOrEmpty(config.UserName, nameof(config.UserName));

            _cacheProvider = cacheProvider;
            _config = config;
            try
            {
                if (string.IsNullOrEmpty(_config.AccessToken))
                {
                    // Public client if no access token is configured.
                    // NOTE: Rate limit is 60 resource calls an hour
                    _client = new ObservableGitHubClient(new ProductHeaderValue(_config.UserName));
                }
                else
                {
                    // Authenticated Client is preferrable
                    // NOTE: Rate limit is 5000 resource calls an hour
                    _client = new ObservableGitHubClient(
                        gitHubClient: new GitHubClient(
                            productInformation: new ProductHeaderValue(_config.UserName))
                        {
                            Credentials = new Credentials(_config.AccessToken)
                        });
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error creating GitHub client for user '{_config.UserName}'", e);
            }

            if (_client == null)
            {
                throw new ArgumentException($"Unable to create GithubClient");
            }
        }

        public async Task<IEnumerable<Commit>> GetRepositoryCommitsAsync(string repositoryName)
             => await _cacheProvider.TryGetEnumerableFromCacheAsync(
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
                                Sha = ghc.Sha,
                                Url = ghc.HtmlUrl
                            })
                            .ToList();

        public async Task<RepoStats> GetRepositoryStatsAsync(string repositoryName)
            => await _cacheProvider.TryGetFromCacheAsync(
                key: GetFormattedCacheKey(repositoryName, "repo-stats"),
                retrievalFunc: () => GetRepositoryStatsAsyncImpl(repositoryName));

        private async Task<RepoStats> GetRepositoryStatsAsyncImpl(string repositoryName)
        {
            var repo = await _client.Repository.Get(_config.UserName, repositoryName);

            var contributors = await _client.Repository.Statistics.GetContributors(_config.UserName, repositoryName)
                .SelectMany(c => c)
                .Select(c => new RepoContributorStats
                {
                    Name = c.Author.Login,
                    TotalCommits = c.Weeks.Sum(w => w.C),
                    WeeklyAverageAdditions = (int)c.Weeks.Average(w => w.A),
                    WeeklyAverageDeletions = (int)c.Weeks.Average(w => w.D),
                    WeeklyAverageCommits = (int)c.Weeks.Average(w => w.C),
                }).ToArray();

            //var langs = await _client.Repository.GetAllLanguages(_config.UserName, repositoryName).ToList();

            //var languages = langs
            //    .Select(l => new RepoLanguage
            //    {
            //        Language = l?.Name,
            //        BytesWritten = (int?)l?.NumberOfBytes ?? 0
            //    })?.ToArray() ?? new RepoLanguage[0];

            return new RepoStats
            {
                CreatedAt = repo.CreatedAt.DateTime,
                LastActivity = repo.UpdatedAt.DateTime,
                Contributors = contributors,
                TotalCommits = contributors.Sum(c => c.TotalCommits),
                //Languages = languages
            };
        }

        public async Task<IEnumerable<RepoContributor>> GetRepositoryContributorsAsync(string repositoryName)
            => await _cacheProvider.TryGetEnumerableFromCacheAsync(
                key: GetFormattedCacheKey(repositoryName, "repo-contrib"),
                retrievalFunc: () => GetRepositoryContributorsAsyncImpl(repositoryName));

        private async Task<IEnumerable<RepoContributor>> GetRepositoryContributorsAsyncImpl(string repositoryName)
        {
            var contributors = await _client
                                         .Repository
                                         .GetAllContributors(
                                            owner: _config.UserName,
                                            name: repositoryName,
                                            includeAnonymous: true)
                                         .Select(c =>
                                             new RepoContributor
                                             {
                                                 Name = c.Type == "Anonymous" ? "Anonymous" : c.Login,
                                                 Contributions = c.Contributions,
                                                 Url = c.HtmlUrl ?? string.Empty
                                             })
                                         .ToList();

            return contributors
                 .GroupBy(c => c.Name)
                 .Select(g => g.Aggregate((agg, e) =>
                     new RepoContributor
                     {
                         Name = agg.Name,
                         Contributions = agg.Contributions + e.Contributions,
                         Url = agg.Url
                     }));
        }

        public Task FlushAsync() => _cacheProvider.ClearAsync();

        public Task<DateTime?> GetKeyExpirationAsync(string key) => _cacheProvider.GetExpirationAsync(key);

        public Task PurgeKeyAsync(string key) => _cacheProvider.RemoveAsync(key);

        public Task<IEnumerable<(string Key, DateTime Expiration)>> GetAllKeysAsync()
            => _cacheProvider.GetAllKeysAsync();

        private string GetFormattedCacheKey(string name, string action) => $"{KeyPrefix}_{action}_{name}";
    }
}
