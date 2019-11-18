using System;
using System.Linq;
using System.Threading.Tasks;
using JoshHarmon.ContentService.Repository.Interface;
using JoshHarmon.Shared.Web;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet("/api/blog")]
        public async Task<IActionResult> GetArticlesMeta([FromQuery] DateTime? from,
            [FromQuery] DateTime? to, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            var startDate = from ?? DateTime.MinValue;
            var endDate = to ?? DateTime.Now;
            var take = limit ?? 10;
            var skip = offset ?? 0;

            var metas = (await _blogRepository.ReadArticleMetasAsync(startDate, endDate)).ToList();
            var totalCount = metas.Count;

            var filteredMetas = metas
                .OrderByDescending(m => m.PublishDate)
                .Skip(skip)
                .Take(take)
                .ToList();

            return Ok(
                new
                {
                    Data = filteredMetas,
                    Page = new Page(
                        from: startDate,
                        to: endDate,
                        offset: skip,
                        total: totalCount)
                });
        }

        [HttpGet("/api/blog/{id}")]
        public async Task<IActionResult> GetArticle(string id)
        {
            var article = await _blogRepository.ReadArticleAsync(id);

            if (article == null)
                return NotFound();

            return Ok(new { Data = new { Article = article } });
        }


        // TODO: Add CRUD endpoints for adding + parsing blog points via webservice
    }
}
