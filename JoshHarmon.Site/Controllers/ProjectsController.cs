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
                    Content = "Josh Harmon's personal website and online tech portfolio. Built on top of " +
                    "AspNetCore with a React frontend. Containerized and deployed via docker image on a self " +
                    "hosted RaspberryPi. As a primarily backend engineer this site represents an effort to " +
                    "become more proficent in frontend design and technologies. While much help and inspiration " +
                    "was found from various online resources -  all code, media assets, and design are 100% " +
                    "original, owned, and copyrighted by Josh Harmon.",
                    IconUrl = "/icon/email-icon-black.png",
                    MediaUrl = "/assets/JoshOutOfFocus.jpg",
                    ExternalUrl = "https://github.com/photo-bro/JoshHarmon",
                    Tools = new []
                    {
                       new Tool {Name = "C#",          ToolType =    ToolType.Language },
                       new Tool {Name = "Javascript",  ToolType =    ToolType.Language },
                       new Tool {Name = "AspNetCore",  ToolType =    ToolType.Framework},
                       new Tool {Name = "React",       ToolType =    ToolType.Framework},
                       new Tool {Name = "Docker",      ToolType =    ToolType.Other    },
                       new Tool {Name = "RaspberryPi", ToolType =    ToolType.Other    },
                    }
                },
                new ProjectModel
                {
                    Name = "Twister",
                    Content = "Twister is a basic programming language heavily modeled after C. " +
                    "This project consists of a basic Twister to x86_64 assembly compiler. All design " +
                    "and code contained currently has been solely created by Josh Harmon. This " +
                    "project is nothing more than a fun and educational exercise.",
                    IconUrl = "/icon/glyph-logo_May2016.png",
                    MediaUrl = "/assets/JoshOutOfFocus.jpg",
                    ExternalUrl ="https://github.com/photo-bro/Twister",
                    Tools = new []
                    {
                         new Tool {Name= "C#",             ToolType= ToolType.Language},
                         new Tool {Name = ".NET Standard", ToolType = ToolType.Other   }
                    }
                }
            };
            return Ok(new { ProjectModels = models });
        }
    }
}
