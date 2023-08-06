using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

using JoshHarmon.Cache;
using JoshHarmon.Cache.Cached.Interface;
using JoshHarmon.Cache.CacheProvider.Interface;
using JoshHarmon.Cache.Interface;
using JoshHarmon.ContentService;
using JoshHarmon.ContentService.Repository;
using JoshHarmon.ContentService.Repository.Interface;
using JoshHarmon.Github;
using JoshHarmon.Github.Interface;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

using Serilog;

var instanceStartTime = DateTime.UtcNow;
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();


/* ** Services configuration ** */
Log.Information("Configuring services for web host.");
builder.Services.AddRouting();
builder.Services.AddResponseCompression(opt =>
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
builder.Services.Configure<GzipCompressionProviderOptions>(opt =>
{
    opt.Level = CompressionLevel.Fastest;
});
builder.Services.Configure<BrotliCompressionProviderOptions>(opt =>
{
    opt.Level = CompressionLevel.Fastest;
});
builder.Services.AddResponseCaching();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


/* ** IOC Wiring ** */
Log.Information("Wiring IOC for web host.");
// NOTE: Manually constructing configuration objects to remaing trimming friendly
builder.Services.AddSingleton<ICacheConfig>(_ =>
{
    var c = configuration.GetRequiredSection("CacheConfig");
    return new CacheConfig
    {
        DefaultExpirationDuration = TimeSpan.Parse(c["DefaultExpirationDuration"] ?? "00:30:00"),
        UseUtc = bool.Parse(c["UseUtc"] ?? "false")
    };
});
builder.Services.AddSingleton<IGithubConfig>(_ =>
{
    var c = configuration.GetRequiredSection("GithubConfig");
    return new GithubConfig
    {
        UserName = c["UserName"],
        AccessToken = c["AccessToken"],
    };
});
builder.Services.AddSingleton<Func<DateTime>>(ctx => () => instanceStartTime);
builder.Services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
builder.Services.AddSingleton<IContentRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var name = configuration["JsonContentPath"] ?? "/DefaultContent.json";
    return new JsonFileContentRespository($"{env.ContentRootPath}{name}");
});
builder.Services.AddSingleton<ICachedContentRepository, CachedContentRepository>();
builder.Services.AddSingleton<IGithubService, CachedGithubService>();
builder.Services.AddSingleton<ICached, CachedContentRepository>();
builder.Services.AddSingleton<ICached, CachedGithubService>();
builder.Services.AddSingleton<IBlogConfig>(_ =>
{
    var c = configuration.GetRequiredSection("BlogConfig");
    return new BlogConfig
    {
        BlogContentPath = c["BlogContentPath"],
    };
});
builder.Services.AddSingleton<IBlogRepository, BlogFileRepository>();

/* ** App configuration  ** */
Log.Information("Building web host.");
var app = builder.Build();
var env = app.Environment;

app.UseResponseCompression();
app.UseResponseCaching();
app.UseRouting();
app.MapControllers();

// app.UseHttpsRedirection(); // Disabled - Using NGINX reverse proxy which will handle https
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age=3600";
    }
});


Log.Information("Configuring web host for environment.");
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

/* ** App Start [Entry Point]  ** */
Log.Information("Starting web host.");
try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
Log.Information("Host terminated successfully.");
return 0;
