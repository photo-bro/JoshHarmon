using System;
namespace JoshHarmon.ContentService.Models
{
    public class ContentModel
    {
        public ContentModel(PanelModel[] panels, ConnectModel[] connections, ProjectModel[] projects)
        {
            Panels = panels ?? throw new ArgumentNullException(nameof(panels));
            Connections = connections ?? throw new ArgumentNullException(nameof(connections));
            Projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        public PanelModel[] Panels { get; }

        public ConnectModel[] Connections { get; }

        public ProjectModel[] Projects { get; }
    }
}
