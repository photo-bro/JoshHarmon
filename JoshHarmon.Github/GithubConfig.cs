using JoshHarmon.Github.Interface;

namespace JoshHarmon.ContentService.Repository
{
    public class GithubConfig : IGithubConfig
    {
        public string? UserName { get; set; }
        public string? AccessToken { get; set; }
    }
}
