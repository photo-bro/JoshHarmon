using JoshHarmon.ContentService.Repository.Interface;

namespace JoshHarmon.ContentService
{
    public class BlogConfig : IBlogConfig
    {
        public string? BlogContentPath { get; set; }
    }
}
