using System;
namespace JoshHarmon.Github.Models
{
    public class RepoLanguage
    {
        public RepoLanguage(string language, int bytesWritten)
        {
            Language = language ?? throw new ArgumentNullException(nameof(language));
            BytesWritten = bytesWritten;
        }

        public string Language { get; }
        public int BytesWritten { get; }
    }
}
