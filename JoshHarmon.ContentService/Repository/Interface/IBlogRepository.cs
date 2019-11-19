using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.ContentService.Models.Blog;

namespace JoshHarmon.ContentService.Repository.Interface
{
    public interface IBlogRepository
    {
        Task<IEnumerable<ArticleMeta>> ReadArticleMetasAsync(DateTime from, DateTime to);

        Task<IEnumerable<Article>> ReadArticlesByDateAsync(DateTime from, DateTime to);

        Task<Article?> ReadArticleByIdAsync(string articleId);

        Task<Article?> ReadArticleByFileKeyAsync(DateTime date, string fileKey);

        Task CheckForNewArticlesAsync();
    }
}
