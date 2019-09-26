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
        public IActionResult HealthCheck()
        {
            var health = "Healthy";
            var machineInfo = $"{Environment.MachineName} {Environment.OSVersion}";
            var serverTime = $"Server Time - {DateTime.Now}";

            return Ok(string.Join(Environment.NewLine, new[] { health, machineInfo, serverTime }));
        }
    }
}
