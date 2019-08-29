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
                    MediaUrl = "/assets/JoshOutOfFocus.jpg",
                    ExternalUrl = "https://github.com/photo-bro/JoshHarmon",
                    Tools = new []
                    {
                        (Name: "C#",         ToolType:    ToolType.Language),
                        (Name: "Javascript", ToolType:    ToolType.Language),
                        (Name: "DotNetCore", ToolType:    ToolType.Framework),
                        (Name: "React",      ToolType:    ToolType.Framework),
                        (Name: "Docker",     ToolType:    ToolType.Other)
                    }
                },
                new ProjectModel
                {
                    Name = "Twister",
                    Content = "Twister is a basic programming language heavily modeled after C. " +
                    "In this repository is a basic Twister to x86_64 assembly compiler. All design " +
                    "and code contained currently has been solely created by Josh Harmon. This " +
                    "project is nothing more than a fun and educational exercise.",
                    IconUrl = "/icon/glyph-logo_May2016.png",
                    MediaUrl = "/assets/JoshOutOfFocus.jpg",
                    ExternalUrl ="",
                    Tools = new []
                    {
                        (Name: "C#",            ToolType: ToolType.Language),
                        (Name: ".NET Standard", ToolType: ToolType.Other)
                    }
                }
            };
            return Ok(new { ProjectModels = models });
        }
    }
}
