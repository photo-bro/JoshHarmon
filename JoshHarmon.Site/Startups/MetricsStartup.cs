using App.Metrics;
using App.Metrics.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JoshHarmon.Site.Startups
{
    public static class MetricsStartup
    {
        public static IServiceCollection AddAppMetrics(this IServiceCollection services,
            IConfiguration configuration)
        {
            var metrics = AppMetrics.CreateDefaultBuilder()
                .Configuration.ReadFrom(configuration)
                .Report.ToTextFile()
                .Build();
            services.AddMetrics(metrics);
            services.AddMetricsEndpoints(configuration);
            services.AddMetricsTrackingMiddleware(configuration);
            services.AddMetricsReportingHostedService();

            return services;
        }

        public static IApplicationBuilder UseAppMetrics(this IApplicationBuilder app)
        {
            app.UseMetricsAllMiddleware();
            app.UseMetricsAllEndpoints();

            return app;
        }
    }
}
