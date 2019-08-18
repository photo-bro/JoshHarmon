using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            return Ok();
        }
    }
}
