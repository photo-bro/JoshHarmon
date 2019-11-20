namespace JoshHarmon.ContentService.Models.Blog
{
    public class Article
    {
        public Article(ArticleMeta meta, string content)
        {
            Meta = meta;
            Content = content;
        }

        public ArticleMeta Meta { get; }

        public string Content { get; }
    }
}
