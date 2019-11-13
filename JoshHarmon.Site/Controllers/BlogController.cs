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
        public async Task<IActionResult> GetArticlesMeta([FromRoute] DateTime? from,
            [FromRoute] DateTime? to, [FromRoute] int? limit)
        {
            var startDate = from ?? DateTime.MinValue;
            var endDate = to ?? DateTime.Now;
            var max = limit ?? 10;

            var metas = (await _blogRepository.ReadArticleMetasAsync(startDate, endDate))
                .Take(max)
                .ToList();

            return Ok(
                new
                {
                    Data = metas,
                    Page = new Page(startDate, endDate, max, metas.Count)
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
