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

        private readonly IBlogConfig _config;
        private readonly IDictionary<string, ArticleMeta> _cachedMeta;
        private readonly IDictionary<string, Article> _cachedContent;

        public BlogFileRepository(IBlogConfig config)
        {
            _config = config;
            _config.BlogContentPath = string.IsNullOrEmpty(config.BlogContentPath) ? "../Blog" : config.BlogContentPath;

            Assert.True(Directory.Exists(_config.BlogContentPath), $"'{nameof(_config.BlogContentPath)}' does not exist");

            _cachedMeta = new Dictionary<string, ArticleMeta>();
            _cachedContent = new Dictionary<string, Article>();

            _ = LoadArticleMetaData();
        }

        private async Task LoadArticleMetaData()
        {
            var allFiles = Directory.GetFiles(_config.BlogContentPath);

            if (allFiles.Length == 0)
                return;

            foreach (var jsonFile in allFiles.Where(f => f.EndsWith(".json", StringComparison.Ordinal)))
            {
                var plainFileName = Path.GetFileNameWithoutExtension(jsonFile);

                // skip articles that don't have correlated content file
                if (!allFiles.Contains($"{_config.BlogContentPath}/{plainFileName}{ContentFileExtension}"))
                    continue;

                // skip duplicates
                if (_cachedMeta.ContainsKey(plainFileName))
                    continue;

                try
                {
                    var rawFileContents = await File.ReadAllTextAsync(jsonFile);
                    var meta = JsonConvert.DeserializeObject<ArticleMeta>(rawFileContents);
                    _cachedMeta.Add(plainFileName, meta);
                }
                catch (Exception e)
                {
                    throw new Exception($"Error reading and deserializing blog article '{jsonFile}'", e);
                }
            }

            _cachedMeta.OrderBy(m => m.Value.PublishDate);
        }

        private async Task<Article> ReadArticleContent(string fileName, ArticleMeta meta)
        {
            var contentPath = $"{_config.BlogContentPath}/{fileName}{ContentFileExtension}";
            Assert.True(File.Exists(contentPath), $"File '{contentPath}' does not exist");

            var rawContent = await File.ReadAllTextAsync(contentPath);

            // TODO: Auto generate summary?
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
            var metas = await ReadArticleMetasAsync(from, to);

            if (metas == null || !metas.Any())
            {
                return new Article[0];
            }

            var articleTasks = metas.Select(m => ReadArticleAsync(m.Id));

            var articles = await Task.WhenAll(articleTasks);

            return articles;
        }

        public async Task<IEnumerable<ArticleMeta>> ReadArticleMetasAsync(DateTime from, DateTime to)
        {
            var metas = _cachedMeta.Where(m => m.Value.PublishDate > from && m.Value.PublishDate <= to);

            if (metas == null || !metas.Any())
            {
                return new ArticleMeta[0];
            }

            return await Task.FromResult(metas.Select(m => m.Value));
        }
    }
}
