using App.Metrics;
using App.Metrics.Counter;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace JoshHarmon.Web.Middleware
{
    public class VisitorTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMetrics _metrics;

        private static readonly string[] IgnorredRoutes = {
            "sockjs-node",
            "assets",
            "css"
        };

        private static readonly string[] IgnorredExtensions = {
            "json",
            "css",
            "js",
            "ico",
            "jpeg",
            "jpg",
            "png"
        };

        public VisitorTrackingMiddleware(RequestDelegate next, IMetrics metrics)
        {
            _next = next;
            _metrics = metrics;
        }

        public async Task InvokeAsync(HttpContext context, IHttpContextAccessor accessor)
        {
            // Filter out endpoints and file types we don't want to track
            var route = context.Request.Path.Value;
            if (IgnorredExtensions.Any(ext => route.EndsWith(ext, System.StringComparison.InvariantCulture)) ||
                IgnorredRoutes.Any(r => route.StartsWith($"/{r}", System.StringComparison.InvariantCulture)))
            {
                await _next(context);
                return;
            }

            var visitorTags = new MetricTags(
                keys: new[] {
                    "IP_Address",
                    "Route",
                    "Method"
                },
                values: new[] {
                    accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    context.Request.Path.Value,
                    context.Request.Method
                });

            var counter = new CounterOptions
            {
                Name = "Visitors",
                Tags = visitorTags,
                MeasurementUnit = Unit.Calls
            };

            _metrics.Measure.Counter.Increment(counter, 1);

            await _next(context);
        }
    }
}
