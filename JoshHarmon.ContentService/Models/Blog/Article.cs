namespace JoshHarmon.ContentService.Models.Blog
{
    public class Article
    {
        public Article(ArticleMeta meta, string summary, string content)
        {
            Meta = meta;
            Summary = summary;
            Content = content;
        }

        ArticleMeta Meta { get; }

        public string Summary { get; }

        public string Content { get; }
    }
}
