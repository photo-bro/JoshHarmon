using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
        private readonly IDictionary<string, ArticleMeta> _cachedMeta;       // Key: FileKey
        private readonly IDictionary<string, Article> _cachedContent;        // Key: ArticleId
        private readonly IDictionary<string, ArticleAssets> _cachedAssets;   // Key: ArticleId
        private bool _loadingArticleMeta = true;

        private static string GenerateFileKey(string jsonFile)
        {
            // TODO: Remove/escape non-html characters
            //       FileKey should be simple and be able to be used in a url cleanly
            var fileName = Path.GetFileNameWithoutExtension(jsonFile);
            fileName = fileName.Replace(' ', '-');
            return fileName;
        }

        public BlogFileRepository(IBlogConfig config)
        {
            _config = config;
            _config.BlogContentPath = string.IsNullOrEmpty(config.BlogContentPath) ? "../Blog" : config.BlogContentPath;

            Assert.True(Directory.Exists(_config.BlogContentPath), $"'{nameof(_config.BlogContentPath)}' does not exist");

            _cachedMeta = new Dictionary<string, ArticleMeta>();
            _cachedContent = new Dictionary<string, Article>();
            _cachedAssets = new Dictionary<string, ArticleAssets>();

            _ = LoadArticleMetaData();

            // spin wait, since await cannot be used in constructor
            while (_loadingArticleMeta)
                Thread.Sleep(10);
        }

        /// <remarks>
        /// Article structure:
        /// root directory
        ///  - ArticleKey/ArticleKey.json    => meta
        ///  - ArticleKey/ArticleKey.content => content
        ///  - ArticleKey/AssetKeys          => assets
        /// </remarks>
        private async Task LoadArticleMetaData()
        {
            var allBlogDirectories = Directory.GetDirectories(_config.BlogContentPath);

            if (allBlogDirectories.Length == 0)
                return;

            foreach (var articleDir in allBlogDirectories)
            {
                var blogFiles = Directory.GetFiles(articleDir);
                var fileKey = GenerateFileKey(articleDir);
                foreach (var jsonFile in blogFiles.Where(f => f.EndsWith(".json", StringComparison.Ordinal)))
                {
                    // skip duplicates
                    if (_cachedMeta.ContainsKey(fileKey))
                        continue;

                    // skip articles that don't have correlated content file
                    if (!blogFiles.Contains($"{_config.BlogContentPath}/{fileKey}/{fileKey}{ContentFileExtension}"))
                        continue;

                    try
                    {
                        var rawFileContents = await File.ReadAllTextAsync(jsonFile);
                        var meta = JsonConvert.DeserializeObject<ArticleMeta>(rawFileContents);

                        meta.FileKey = fileKey;
                        _cachedMeta.Add(fileKey, meta);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Error reading and deserializing blog article '{jsonFile}'", e);
                    }
                }
            }

            _cachedMeta.OrderBy(m => m.Value.PublishDate);
            _loadingArticleMeta = false;
        }

        private async Task<Article> ReadArticleContent(string fileName, ArticleMeta meta)
        {
            var contentPath = $"{_config.BlogContentPath}/{fileName}/{fileName}{ContentFileExtension}";
            Assert.True(File.Exists(contentPath), $"File '{contentPath}' does not exist");

            var rawContent = await File.ReadAllTextAsync(contentPath);

            var article = new Article(meta, rawContent);

            _cachedContent.Add(meta.Id, article);

            return article;
        }

        public async Task<Article?> ReadArticleByIdAsync(string articleId)
        {
            if (_cachedContent.ContainsKey(articleId))
                return _cachedContent[articleId];

            if (!_cachedMeta.Any(m => m.Value.Id == articleId))
                return null;

            var metaFileNamePair = _cachedMeta.First(m => m.Value.Id == articleId);

            var article = await ReadArticleContent(metaFileNamePair.Key, metaFileNamePair.Value);

            return article;
        }

        private ArticleMeta? GetMetaFromDateAndFileKey(DateTime date, string fileKey)
        {
            var metas = _cachedMeta
               .Where(m =>
                   date.Year == m.Value.PublishDate.Year &&
                   date.Month == m.Value.PublishDate.Month &&
                   date.Day == m.Value.PublishDate.Day)
               .Where(m => m.Key == fileKey)
               .ToList();

            if (metas == null || metas.Count == 0)
                return null;

            if (metas.Count > 1)
                throw new Exception($"Duplicate articles with filename '{fileKey}', unable to read article.");

            var meta = metas.First().Value;

            return meta;
        }

        public async Task<Article?> ReadArticleByFileKeyAsync(DateTime date, string fileKey)
        {
            var meta = GetMetaFromDateAndFileKey(date, fileKey);

            if (meta == null)
                return null;

            return await ReadArticleByIdAsync(meta.Id);
        }

        private async Task<byte[]?> ReadArticleAsset(ArticleMeta meta, string assetKey)
        {
            ArticleAssets? cachedAssets = null;
            if (_cachedAssets.ContainsKey(meta.Id))
            {
                cachedAssets = _cachedAssets[meta.Id];
                var (_, foundAssetBytes) = cachedAssets.Assets.FirstOrDefault(a => a.AssetKey == assetKey);
                if (foundAssetBytes != default)
                    return foundAssetBytes;
            }

            var articlePath = $"{_config.BlogContentPath}/{meta.FileKey}/";

            var assetFileNames = Directory.GetFiles(articlePath)
                .Select(Path.GetFileName)
                .Where(f => string.Equals(f, assetKey, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            if (assetFileNames.Count == 0)
                return null;

            if (assetFileNames.Count > 1)
                throw new Exception($"Duplicate assets with filename '{assetKey}'");

            if (cachedAssets == null)
            {
                cachedAssets = new ArticleAssets(meta.Id, meta.FileKey, new List<(string, byte[])>());
                _cachedAssets.Add(meta.Id, cachedAssets);
            }

            var assetPath = $"{articlePath}{assetFileNames[0]}";
            Assert.True(File.Exists(assetPath), $"Asset with path and filename '{assetFileNames[0]}' could not be found");

            try
            {
                var file = await File.ReadAllBytesAsync(assetPath);
                cachedAssets.Assets.Add((assetFileNames[0], file));
                return file;
            }
            catch (Exception e)
            {
                throw new Exception($"Error reading asset with filename '{assetFileNames[0]}'", e);
            }
        }

        public async Task<byte[]?> ReadArticleAssetByKeyAsync(DateTime date, string fileKey, string assetKey)
        {
            var meta = GetMetaFromDateAndFileKey(date, fileKey);

            if (meta == null)
                return null;

            var asset = await ReadArticleAsset(meta, assetKey);

            return asset;
        }

        public async Task<IEnumerable<Article>> ReadArticlesByDateAsync(DateTime from, DateTime to)
        {
            var metas = await ReadArticleMetasAsync(from, to);

            if (metas == null || !metas.Any())
            {
                return new Article[0];
            }

            var articleTasks = metas.Select(m => ReadArticleByIdAsync(m.Id));

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

        public async Task CheckForNewArticlesAsync() => await LoadArticleMetaData();

    }
}
