using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.Github.Interface;
using JoshHarmon.Github.Models;
using JoshHarmon.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JoshHarmon.Site.Controllers
{
    public class GithubFeedController : Controller
    {
        private readonly IGithubService _githubService;
        private readonly ILogger<GithubFeedController> _logger;

        public GithubFeedController(IGithubService githubService, ILogger<GithubFeedController> logger)
        {
            Assert.NotNull(githubService);
            Assert.NotNull(logger);

            _githubService = githubService;
            _logger = logger;
        }

        [HttpGet("/api/github/{repositoryName}/stats")]
        public async Task<IActionResult> GetRepositoryStats(string repositoryName)
        {
            RepoStats stats;
            try
            {
                stats = await _githubService.GetRepositoryStatsAsync(repositoryName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving statistics for GitHub repository '{Repository}'",
                    repositoryName);
                return new StatusCodeResult(500);
            }


            return Ok(new { Stats = stats });
        }


        [HttpGet("/api/github/{repositoryName}/commits")]
        public async Task<IActionResult> GetRepositoryCommits(string repositoryName)
        {
            IEnumerable<Commit> commits;
            try
            {
                commits = await _githubService.GetRepositoryCommitsAsync(repositoryName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving commit information for GitHub repository '{Repository}'",
                    repositoryName);
                return new StatusCodeResult(500);
            }


            return Ok(new { Commits = commits });
        }
    }
}
