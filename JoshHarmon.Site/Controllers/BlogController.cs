using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoshHarmon.ContentService.Models.Blog;
using JoshHarmon.ContentService.Repository;
using JoshHarmon.ContentService.Repository.Interface;
using JoshHarmon.Shared.Web;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController()
        {
            _blogRepository = new BlogFileRepository();
        }


        [HttpGet("/api/blog")]
        public async Task<IActionResult> GetArticles([FromRoute] DateTime? from, [FromRoute] DateTime? to, [FromRoute] int? limit)
        {
            var startDate = from ?? DateTime.MinValue;
            var endDate = to ?? DateTime.Now;
            var max = limit ?? 10;

            var articles = (await _blogRepository.ReadArticlesByDateAsync(startDate, endDate)).Take(max).ToList();


            return Ok(
                new
                {
                    Data = articles,
                    Page = new Page(startDate, endDate, max, articles.Count)
                });
        }

        [HttpGet("/api/blog/{id}")]
        public async Task<IActionResult> GetArticle(string id)
        {
            var article = await _blogRepository.ReadArticleAsync(id);

            if (article == null)
                return NotFound();

            return Ok(new { Article = article });
        }

    }
}
