using System;
namespace JoshHarmon.Site.Models
{
    public class SplashModel
    {
        public PanelModel[] PanelModels { get; set; }

        public ConnectModel ConnectModel { get; set; }
    }

    public class PanelModel
    {
        public string MediaUrl { get; set; }

        public string LinkUrl { get; set; }

        public string Title { get; set; }
    }

    public class ConnectModel
    {
        public ConnectIcons[] Icons { get; set; }
    }

    public class ConnectIcons
    {
        public string IconUrl { get; set; }

        public string LinkUrl { get; set; }
    }
}
