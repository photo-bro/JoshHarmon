﻿using System;
namespace JoshHarmon.ContentService.Models.Blog
{
    public class ArticleMeta
    {
        public ArticleMeta(string id, string title, string? author, DateTime publishDate,
            string? bannerMediaPath, string[]? tags, string? summary)
        {
            Id = id;
            Title = title;
            Author = author;
            PublishDate = publishDate;
            BannerMediaPath = bannerMediaPath;
            Tags = tags ?? new string[0];
            Summary = summary;
        }

        public string Id { get; }

        public string Title { get; }

        public string? Author { get; }

        public DateTime PublishDate { get; }

        public string? BannerMediaPath { get; }

        public string[] Tags { get; }

        public string? Summary { get; }
    }
}
