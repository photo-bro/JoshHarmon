using System;
namespace JoshHarmon.Github.Models
{
    public class Commit
    {
        public string CommitterName { get; set; }

        public string CommitterEmail { get; set; }

        public DateTime DateTime { get; set; }

        public string Message { get; set; }

        public string Sha { get; set; }

        public string Url { get; set; }
    }
}
