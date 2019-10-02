using System;
namespace JoshHarmon.Github.Models
{
    public class Commit
    {
        public Commit(string committerName, string committerEmail, DateTime dateTime,
            string message, string sha, string url)
        {
            CommitterName = committerName ?? throw new ArgumentNullException(nameof(committerName));
            CommitterEmail = committerEmail ?? throw new ArgumentNullException(nameof(committerEmail));
            DateTime = dateTime;
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Sha = sha ?? throw new ArgumentNullException(nameof(sha));
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public string CommitterName { get; }

        public string CommitterEmail { get; }

        public DateTime DateTime { get; }

        public string Message { get; }

        public string Sha { get; }

        public string Url { get; }
    }
}
