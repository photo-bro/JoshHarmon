using JoshHarmon.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    [Route("api/splash")]
    public class SplashController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            var model = new SplashModel
            {
                PanelModels = new[]
                {
                    new PanelModel
                    {
                        Title = "Visual Artist",
                        MediaUrl = "https://images.squarespace-cdn.com/content/v1/54009568e4b0f2da40182573/1547348618724-2JKFK89CIT57GOIKVMKI/ke17ZwdGBToddI8pDm48kC2ej-KCx4NUeXBrpVplsP57gQa3H78H3Y0txjaiv_0fDoOvxcdMmMKkDsyUqMSsMWxHk725yiiHCCLfrh8O1z4YTzHvnKhyp6Da-NYroOW3ZGjoBKy3azqku80C789l0kMlYkjvFlctRdmAM11rxFT8aoEn_zHl7xldrVuGo2Anx8-bJbySI6vMrsCJOa_h6w/Lone+Aspen+at+Dawn.jpg?format=2500w",
                        LinkUrl = "https://joshharmon.co/"
                    },
                    new PanelModel
                    {
                        Title = "Engineer",
                        MediaUrl = "https://live.staticflickr.com/572/20607150556_c01d092437_b.jpg",
                        LinkUrl = "https://github.com/photo-bro/"
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
