namespace JoshHarmon.Github.Models
{
    public class RepoContributorStats
    {
        public string Name { get; set; }
        public int TotalCommits { get; set; }
        public int WeeklyAverageAdditions { get; set; }
        public int WeeklyAverageDeletions { get; set; }
        public int WeeklyAverageCommits { get; set; }
        public string Url { get; set; }
    }
}
