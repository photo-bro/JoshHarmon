using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.ContentService.Models.Blog;
using JoshHarmon.ContentService.Repository.Interface;

namespace JoshHarmon.ContentService.Repository
{
    public class BlogFileRepository : IBlogRepository
    {
        public async Task<Article?> ReadArticleAsync(string articleId)
        {
            if (articleId == "1")
                return await Task.FromResult(new Article("1", "TestArticle", DateTime.Now));
            return null;
        }

        public async Task<IEnumerable<Article>> ReadArticlesByDateAsync(DateTime from, DateTime to)
        {
            return await Task.FromResult(new[] { new Article("1", "TestArticle", DateTime.Now) });
        }
    }
}
