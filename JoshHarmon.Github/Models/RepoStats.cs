using System;
namespace JoshHarmon.Github.Models
{
    public class RepoStats
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastActivity { get; set; }
        public int TotalCommits { get; set; }
        public RepoContributor[] Contributors { get; set; }
    }

    public class RepoContributor
    {
        public string Name { get; set; }
        public int TotalCommits { get; set; }
        public int WeeklyAverageAdditions { get; set; }
        public int WeeklyAverageDeletions { get; set; }
        public int WeeklyAverageCommits { get; set; }
    }
}
