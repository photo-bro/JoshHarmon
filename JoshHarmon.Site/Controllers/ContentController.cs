using JoshHarmon.ContentService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;

        public ContentController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        [HttpGet("api/splash")]
        public IActionResult GetPanels()
        {
            var models = _contentRepository.ReadAllPanelModels();

            return Ok(new { Panels = models });
        }

        [HttpGet("api/connections")]
        public IActionResult GetConnections()
        {
            var models = _contentRepository.ReadAllConnectModels();

            return Ok(new { Connections = models });
        }

        [HttpGet("api/projects")]
        public IActionResult GetProjects()
        {
            var models = _contentRepository.ReadAllProjectModels();

            return Ok(new { Projects = models });
        }
    }
}
