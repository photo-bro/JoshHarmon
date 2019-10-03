using System;

namespace JoshHarmon.ContentService.Models
{
    public class ProjectModel
    {
        public ProjectModel(string name, string repositoryName, string iconUrl, string mediaUrl, string content, Tool[] tools, string externalUrl)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            RepositoryName = repositoryName ?? throw new ArgumentNullException(nameof(repositoryName));
            IconUrl = iconUrl ?? throw new ArgumentNullException(nameof(iconUrl));
            MediaUrl = mediaUrl ?? throw new ArgumentNullException(nameof(mediaUrl));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Tools = tools ?? throw new ArgumentNullException(nameof(tools));
            ExternalUrl = externalUrl ?? throw new ArgumentNullException(nameof(externalUrl));
        }

        public string Name { get; }

        public string RepositoryName { get; }

        public string IconUrl { get; }

        public string MediaUrl { get; }

        public string Content { get; }

        public Tool[] Tools { get; }

        public string ExternalUrl { get; }
    }

    public class Tool
    {
        public Tool(string name, ToolType toolType)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ToolType = toolType;
        }

        public string Name { get; }

        public ToolType ToolType { get; }
    }

    public enum ToolType
    {
        Language = 0,
        Framework,
        Other
    }
}
