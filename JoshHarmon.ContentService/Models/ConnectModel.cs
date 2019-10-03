using System;

namespace JoshHarmon.ContentService.Models
{
    public class ConnectModel
    {
        public ConnectModel(string name, string iconUrl, string linkUrl)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IconUrl = iconUrl ?? throw new ArgumentNullException(nameof(iconUrl));
            LinkUrl = linkUrl ?? throw new ArgumentNullException(nameof(linkUrl));
        }

        public string Name { get; }

        public string IconUrl { get; }

        public string LinkUrl { get; }
    }
}
