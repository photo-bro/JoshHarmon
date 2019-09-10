namespace JoshHarmon.ContentService.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public string IconUrl { get; set; }

        public string MediaUrl { get; set; }

        public string Content { get; set; }

        public Tool[] Tools { get; set; }

        public string ExternalUrl { get; set; }
    }

    public class Tool
    {
        public string Name { get; set; }

        public ToolType ToolType { get; set; }
    }

    public enum ToolType
    {
        Language = 0,
        Framework,
        Other
    }
}
