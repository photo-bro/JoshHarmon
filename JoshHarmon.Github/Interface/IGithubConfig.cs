namespace JoshHarmon.Github.Interface
{
    public interface IGithubConfig
    {
        string? UserName { get; }

        string? AccessToken { get; }
    }
}
