using System;
namespace JoshHarmon.Site.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public string IconUrl { get; set; }

        public string MediaUrl { get; set; }

        public string Content { get; set; }

        public (string Name, ToolType ToolType)[] Tools { get; set; }

        public string ExternalUrl { get; set; }
    }

    public enum ToolType
    {
        Language = 0,
        Framework,
        Other
    }
}
