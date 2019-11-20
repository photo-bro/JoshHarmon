using System.Collections.Generic;

namespace JoshHarmon.ContentService.Models.Blog
{
    public class ArticleAssets
    {
        public ArticleAssets(string articleId, string articleFileKey, IList<(string AssetKey, byte[] AssetBytes)> assets)
        {
            ArticleId = articleId;
            ArticleFileKey = articleFileKey;
            Assets = assets;
        }

        public string ArticleId { get; }

        public string ArticleFileKey { get; }

        public IList<(string AssetKey, byte[] AssetBytes)> Assets { get; }
    }
}
