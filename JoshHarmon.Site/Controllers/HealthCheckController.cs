using System;
using Microsoft.AspNetCore.Mvc;

namespace JoshHarmon.Site.Controllers
{
    public class HealthCheckController : Controller
    {
        private readonly DateTime _instanceStart;

        public HealthCheckController(Func<DateTime> getInstanceStartTime)
        {
            _instanceStart = getInstanceStartTime();
        }

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
            var serverTime = $"Server Time - {DateTime.Now:O}";
            var upTime = $"Instance up time - {DateTime.UtcNow - _instanceStart}";

            return Ok(string.Join(Environment.NewLine, new[] { health, machineInfo, serverTime, upTime }));
        }
    }
}
