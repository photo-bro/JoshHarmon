using System;

namespace JoshHarmon.Github.Models
{
    public class RepoContributorStats
    {
        public RepoContributorStats(string name, int totalCommits, int weeklyAverageAdditions,
            int weeklyAverageDeletions, int weeklyAverageCommits, string? url)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TotalCommits = totalCommits;
            WeeklyAverageAdditions = weeklyAverageAdditions;
            WeeklyAverageDeletions = weeklyAverageDeletions;
            WeeklyAverageCommits = weeklyAverageCommits;
            Url = url;
        }

        public string Name { get; }
        public int TotalCommits { get; }
        public int WeeklyAverageAdditions { get; }
        public int WeeklyAverageDeletions { get; }
        public int WeeklyAverageCommits { get; }
        public string? Url { get; }
    }
}
