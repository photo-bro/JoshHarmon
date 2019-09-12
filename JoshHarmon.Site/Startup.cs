using System.IO;
using JoshHarmon.Cache;
using JoshHarmon.Cache.Interface;
using JoshHarmon.ContentService.Repository;
using JoshHarmon.ContentService.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JoshHarmon.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
            services.AddSingleton<IContentRepository>(sp =>
            {
                var env = sp.GetRequiredService<IHostingEnvironment>();
                var name = Configuration.GetValue<string>("JsonContentPath");
                var logger = sp.GetRequiredService<ILogger<JsonFileContentRespository>>();
                var cachedProvider = sp.GetRequiredService<ICacheProvider>();
                return new CachedJsonFileContentRespository($"{env.ContentRootPath}{name}", logger, cachedProvider);
            });
            services.AddSingleton<ICached, CachedJsonFileContentRespository>();
        }
    }
}
