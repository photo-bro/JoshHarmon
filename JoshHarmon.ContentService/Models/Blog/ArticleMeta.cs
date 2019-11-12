using System;
namespace JoshHarmon.ContentService.Models.Blog
{
    public class ArticleMeta
    {
        public ArticleMeta(string id, string title, DateTime publishDate, string? author, string[]? tags)
        {
            Id = id;
            Title = title;
            PublishDate = publishDate;
            Tags = tags ?? new string[0];
        }

        public string Id { get; }

        public string Title { get; }

        public DateTime PublishDate { get; }

        public string? Author { get; }

        public string[] Tags { get; }
    }
}
