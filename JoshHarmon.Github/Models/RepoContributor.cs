using System;
namespace JoshHarmon.Github.Models
{
    public class RepoContributor
    {
        public RepoContributor(string name, int contributions, string url)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Contributions = contributions;
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public string Name { get; }
        public int Contributions { get; }
        public string Url { get; }
    }
}
