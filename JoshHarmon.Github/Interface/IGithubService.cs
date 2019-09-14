using System.Threading.Tasks;
using JoshHarmon.Github.Models;
using Octokit;

namespace JoshHarmon.Github.Interface
{
    public interface IGithubService
    {
        Task<EventInfo> GetRepositoryEvents(string repositoryName);


        Task<RepoStats> GetRepositoryStats(string repositoryName);
    }
}
