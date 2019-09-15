using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.Github.Models;
using Octokit;
using Commit = JoshHarmon.Github.Models.Commit;

namespace JoshHarmon.Github.Interface
{
    public interface IGithubService
    {
        Task<IEnumerable<Commit>> GetRepositoryCommitsAsync(string repositoryName);

        Task<RepoStats> GetRepositoryStatsAsync(string repositoryName);
    }
}
