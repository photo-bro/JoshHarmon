using System;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    public class HealthCheckController : Controller
    {
        [HttpGet]
        [Route("/ping")]
        [Route("/api/ping")]
        public IActionResult Ping() => Ok();


        [HttpGet]
        [Route("/healthcheck")]
        [Route("/api/healthcheck")]
        public IActionResult HealthCheck() => Ok($"Healthy - {DateTime.UtcNow}");
    }
}
