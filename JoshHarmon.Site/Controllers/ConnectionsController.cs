using JoshHarmon.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    [Route("api/connections")]
    public class ConnectionsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var connectModel = new ConnectModel
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
            };

            return Ok(new { ConnectModel = connectModel });
        }
    }
}
