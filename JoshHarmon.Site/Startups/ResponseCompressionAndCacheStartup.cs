using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace JoshHarmon.Site.Startups
{
    public static class ResponseCompressionAndCacheStartup
    {
        public static IServiceCollection AddConfiguredReponseCompressionAndCaching(this IServiceCollection services)
        {
            services.AddResponseCompression(opt =>
            {
                opt.Providers.Add<BrotliCompressionProvider>();
                opt.Providers.Add<GzipCompressionProvider>();
                opt.EnableForHttps = true;
                opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {
                    "application/xhtml+xml",
                    "application/atom+xml",
                    "image/svg+xml",
                });
            });
            services.Configure<GzipCompressionProviderOptions>(opt =>
            {
                opt.Level = CompressionLevel.Fastest;
            });
            services.Configure<BrotliCompressionProviderOptions>(opt =>
            {
                opt.Level = CompressionLevel.Fastest;
            });
            services.AddResponseCaching();

            return services;
        }

        public static IApplicationBuilder UseConfiguredResponseCompressionAndCaching(this IApplicationBuilder app)
        {
            app.UseResponseCompression();
            app.UseResponseCaching();
            return app;
        }
    }
}
