using System;
namespace JoshHarmon.Github.Models
{
    public class RepoStats
    {
        public RepoStats(DateTime createdAt,
                         DateTime lastActivity,
                         int totalCommits,
                         RepoContributorStats[] contributors,
                         RepoLanguage[] languages,
                         string readme)
        {
            CreatedAt = createdAt;
            LastActivity = lastActivity;
            TotalCommits = totalCommits;
            Contributors = contributors ?? throw new ArgumentNullException(nameof(contributors));
            Languages = languages ?? throw new ArgumentNullException(nameof(languages));
            Readme = readme;
        }

        public DateTime CreatedAt { get; }
        public DateTime LastActivity { get; }
        public int TotalCommits { get; }
        public RepoContributorStats[] Contributors { get; }
        public RepoLanguage[] Languages { get; }
        public string Readme { get; }
    }
}
