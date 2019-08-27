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
            var model = new SplashModel
            {
                PanelModels = new[]
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
                },
                ConnectModel = new ConnectModel
                {
                    Icons = new[]
                    {
                        new ConnectIcons
                        {
                            Name = "email",
                            LinkUrl = "mailto:joshuapharmon@gmail.com",
                            IconUrl= "/icon/email-icon-black.png"
                        },
                        new ConnectIcons
                        {
                            Name = "GitHub",
                            LinkUrl = "https://www.github.com/photo-bro/",
                            IconUrl= "/icon/GitHub-Mark-120px-plus.png"
                        },
                        new ConnectIcons
                        {
                            Name = "LinkedIn",
                            LinkUrl = "https://www.linkedin.com/in/joshuapharmon/",
                            IconUrl= "/icon/LI-In-Bug Black.png"
                        }
                    }
                }
            };
            return Ok(model);
        }
    }
}
