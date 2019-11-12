using System;
namespace JoshHarmon.ContentService.Models.Blog
{
    public class Article
    {
        public Article(string id, string title, DateTime publishDate)
        {
            Id = id;
            Title = title;
            PublishDate = publishDate;
        }

        public string Id { get; }

        public string Title { get; }

        public DateTime PublishDate { get; }

        public string? Summary { get; set; }

        public string? Content { get; set; }

    }
}
