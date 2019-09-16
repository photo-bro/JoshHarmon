using System;
namespace JoshHarmon.Github.Models
{
    public class RepoStats
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastActivity { get; set; }
        public int TotalCommits { get; set; }
        public RepoContributorStats[] Contributors { get; set; }
        public RepoLanguage[] Languages { get; set; }
    }
}
