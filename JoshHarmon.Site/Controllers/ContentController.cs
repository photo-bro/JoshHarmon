using System.Threading.Tasks;
using JoshHarmon.ContentService.Repository.Interface;
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
        public async Task<IActionResult> GetPanels()
        {
            var models = await _contentRepository.ReadAllPanelModels();

            return Ok(new { Panels = models });
        }

        [HttpGet("api/connections")]
        public async Task<IActionResult> GetConnections()
        {
            var models = await _contentRepository.ReadAllConnectModels();

            return Ok(new { Connections = models });
        }

        [HttpGet("api/projects")]
        public async Task<IActionResult> GetProjects()
        {
            var models = await _contentRepository.ReadAllProjectModels();

            return Ok(new { Projects = models });
        }
    }
}
