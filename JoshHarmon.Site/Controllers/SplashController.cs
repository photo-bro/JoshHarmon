using JoshHarmon.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    [Route("api/splash")]
    public class SplashController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var models = new[]
                {
                    new PanelModel
                    {
                        Title = "Visual Artist",
                        MediaUrl = "/assets/Lone+Aspen+at+Dawn.jpg",
                        LinkUrl = "https://joshharmon.co/"
                    },
                    new PanelModel
                    {
                        Title = "Projects",
                        MediaUrl = "/assets/MedusasBaneLow.png",
                        LinkUrl = "/projects"
                    },
                    new PanelModel
                    {
                        Title = "About",
                        MediaUrl = "/assets/JoshOutOfFocus.jpg",
                        LinkUrl = "https://www.joshharmonimages.com/about"
                    }
            };
            return Ok(new { PanelModels = models });
        }
    }
}
