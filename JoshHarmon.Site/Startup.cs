using System;
using JoshHarmon.Cache;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.Cache.Interface;
using JoshHarmon.ContentService.Repository;
using JoshHarmon.ContentService.Repository.Interface;
using JoshHarmon.Github;
using JoshHarmon.Github.Interface;
using JoshHarmon.Site.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JoshHarmon.Site
{
    public class Startup
    {
        private readonly DateTime _instanceStartTime;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _instanceStartTime = DateTime.UtcNow;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddConfiguredReponseCompressionAndCaching();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAppMetrics(Configuration);
            services.AddConfiguredStaticFiles();

            ConfigureIoc(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseConfiguredResponseCompressionAndCaching();
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

            app.UseAppMetrics();
            app.UseConfiguredStaticFiles(env);
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
