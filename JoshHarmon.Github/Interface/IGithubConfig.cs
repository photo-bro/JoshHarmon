using System;
namespace JoshHarmon.Github.Interface
{
    public interface IGithubConfig
    {
        string UserName { get; set; }

        string AccessToken { get; set; }
    }
}
