using System.Collections.Generic;
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
//using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JoshHarmon.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            ConfigureIoc(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc();

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
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();

            services.AddSingleton<IContentRepository>(sp =>
            {
                var env = sp.GetRequiredService<IHostingEnvironment>();
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
