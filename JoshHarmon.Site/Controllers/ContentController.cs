﻿using System.Threading.Tasks;
using JoshHarmon.ContentService.Repository.Interface;
using JoshHarmon.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    public class ContentController : Controller
    {
        private readonly ICachedContentRepository _contentRepository;

        public ContentController(ICachedContentRepository contentRepository)
        {
            Assert.NotNull(contentRepository, nameof(contentRepository));

            _contentRepository = contentRepository;
        }

        [ResponseCache(Duration = 60 * 60 * 24)]
        [HttpGet("api/splash")]
        public async Task<IActionResult> GetPanels()
        {
            var models = await _contentRepository.ReadAllPanelModels();

            return Ok(new { Panels = models });
        }

        [ResponseCache(Duration = 60 * 60 * 24)]
        [HttpGet("api/connections")]
        public async Task<IActionResult> GetConnections()
        {
            var models = await _contentRepository.ReadAllConnectModels();

            return Ok(new { Connections = models });
        }

        [ResponseCache(Duration = 60 * 60 * 24)]
        [HttpGet("api/projects")]
        public async Task<IActionResult> GetProjects()
        {
            var models = await _contentRepository.ReadAllProjectModels();

            return Ok(new { Projects = models });
        }
    }
}
