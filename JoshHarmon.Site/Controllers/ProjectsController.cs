using JoshHarmon.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var models = new[]
            {
                new ProjectModel
                {
                    Name = "JoshHarmon.Site",
                    Content = "This website",
                    IconUrl = "/icon/email-icon-black.png",
                    MediaUrl = "/assets/JoshOutOfFocus.jpg"
                },
                new ProjectModel
                {
                    Name = "Twister",
                    Content = "Basic Compiler for a C like language called Twister. Built for fun and educational purposes.\n",
                    IconUrl = "/icon/glyph-logo_May2016.png",
                    MediaUrl = "/assets/JoshOutOfFocus.jpg"
                }
            };
            return Ok(new { ProjectModels = models });
        }
    }
}
