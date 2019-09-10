using System;
namespace JoshHarmon.ContentService.Models
{
    public class ContentModel
    {
        public PanelModel[] Panels { get; set; }

        public ConnectModel[] Connections { get; set; }

        public ProjectModel[] Projects { get; set; }
    }
}
