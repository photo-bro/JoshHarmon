using System;
using System.IO.Compression;
using System.Linq;
using JoshHarmon.Cache;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.Cache.Interface;
using JoshHarmon.ContentService.Repository;
using JoshHarmon.ContentService.Repository.Interface;
using JoshHarmon.Github;
using JoshHarmon.Github.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace JoshHarmon.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _instanceStartTime = DateTime.UtcNow;
        }

        public IConfiguration Configuration { get; }

        private readonly DateTime _instanceStartTime;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

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

            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            ConfigureIoc(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            app.UseResponseCaching();

            app.UseRouting();
            app.UseEndpoints(conf => { conf.MapControllers(); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection(); // Disabled - Using NGINX reverse proxy which will handle https
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age=3600";
                }
            });
            app.UseSpaStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age=3600";
                }
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private void ConfigureIoc(IServiceCollection services)
        {
            services.AddSingleton<ICacheConfig>(Configuration.GetSection("CacheConfig").Get<CacheConfig>());
            services.AddSingleton<IGithubConfig>(Configuration.GetSection("GithubConfig").Get<GithubConfig>());
            services.AddSingleton<Func<DateTime>>(ctx => () => _instanceStartTime);
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();

            services.AddSingleton<IContentRepository>(sp =>
            {
                var env = sp.GetRequiredService<IWebHostEnvironment>();
                var name = Configuration.GetValue<string>("JsonContentPath");
                return new JsonFileContentRespository($"{env.ContentRootPath}{name}");
            });
            services.AddSingleton<ICachedContentRepository, CachedContentRepository>();
            services.AddSingleton<IGithubService, CachedGithubService>();

            services.AddSingleton<ICached, CachedContentRepository>();
            services.AddSingleton<ICached, CachedGithubService>();
        }
    }
}
