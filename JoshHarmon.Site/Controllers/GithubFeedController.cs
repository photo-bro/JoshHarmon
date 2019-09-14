using System;
using System.Threading.Tasks;
using JoshHarmon.Github.Interface;
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
            _githubService = githubService;
            _logger = logger;
        }

        [HttpGet("/api/github/feeds/{repositoryName}")]
        public async Task<IActionResult> GetRepositoryActivity(string repositoryName)
        {
            Octokit.EventInfo feed;
            try
            {
                feed = await _githubService.GetRepositoryEvents(repositoryName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving event feed for GitHub repository '{Repository}'", repositoryName);
                return new StatusCodeResult(500);
            }


            return Ok(feed);
        }
    }
}
