using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JoshHarmon.ContentService.Models.Blog;
using JoshHarmon.ContentService.Repository.Interface;
using JoshHarmon.Shared;
using Newtonsoft.Json;

namespace JoshHarmon.ContentService.Repository
{
    public class BlogFileRepository : IBlogRepository
    {
        const string ContentFileExtension = ".content";

        private readonly string _blogRootDirectory;
        private IDictionary<string, ArticleMeta> _cachedMeta;
        private IDictionary<string, Article> _cachedContent;

        public BlogFileRepository(BlogConfig config)
        {
            _blogRootDirectory = config.BlogArticlesPath ?? "../Blog";

            Assert.True(Directory.Exists(_blogRootDirectory), $"'{nameof(_blogRootDirectory)}' does not exist");

            _cachedMeta = new Dictionary<string, ArticleMeta>();
            _cachedContent = new Dictionary<string, Article>();

            _ = LoadArticleMetaData();
        }

        private async Task LoadArticleMetaData()
        {
            var allFiles = Directory.GetFiles(_blogRootDirectory);

            if (allFiles.Length == 0)
                return;

            foreach (var jsonFile in allFiles.Where(f => f.EndsWith(".json", StringComparison.Ordinal)))
            {
                var plainFileName = Path.GetFileNameWithoutExtension(jsonFile);

                // skip articles that don't have content
                if (!allFiles.Contains($"{plainFileName}{ContentFileExtension}"))
                    continue;

                // skip duplicates
                if (_cachedMeta.ContainsKey(plainFileName))
                    continue;

                try
                {
                    var rawFileContents = await File.ReadAllTextAsync(jsonFile);
                    var meta = JsonConvert.DeserializeObject<ArticleMeta>(rawFileContents);
                    _cachedMeta.Add(Path.GetFileNameWithoutExtension(jsonFile), meta);
                }
                catch (Exception e)
                {
                    throw new Exception($"Error reading and deserializing blog article '{jsonFile}'", e);
                }
            }
        }

        private async Task<Article> ReadArticleContent(string fileName, ArticleMeta meta)
        {
            var contentPath = $"{_blogRootDirectory}{fileName}{ContentFileExtension}";
            Assert.True(File.Exists(contentPath), $"File '{contentPath}' does not exist");

            var rawContent = await File.ReadAllTextAsync(contentPath);

            var article = new Article(meta, string.Empty, rawContent);

            _cachedContent.Add(meta.Id, article);

            return article;
        }


        public async Task<Article?> ReadArticleAsync(string articleId)
        {
            if (_cachedContent.ContainsKey(articleId))
                return _cachedContent[articleId];

            if (!_cachedMeta.Any(m => m.Value.Id == articleId))
                return null;

            var metaFileNamePair = _cachedMeta.First(m => m.Value.Id == articleId);

            var article = await ReadArticleContent(metaFileNamePair.Key, metaFileNamePair.Value);

            return article;
        }

        public async Task<IEnumerable<Article>> ReadArticlesByDateAsync(DateTime from, DateTime to)
        {
            var metas = _cachedMeta.Where(m => m.Value.PublishDate > from && m.Value.PublishDate <= to);

            var articleTasks = metas.Select(m => ReadArticleAsync(m.Value.Id));

            var articles = await Task.WhenAll(articleTasks);

            return articles;
        }

    }
}
