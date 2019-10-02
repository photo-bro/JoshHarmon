using System;

namespace JoshHarmon.ContentService.Models
{
    public class PanelModel
    {
        public PanelModel(string mediaUrl, string linkUrl, string title)
        {
            MediaUrl = mediaUrl ?? throw new ArgumentNullException(nameof(mediaUrl));
            LinkUrl = linkUrl ?? throw new ArgumentNullException(nameof(linkUrl));
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string MediaUrl { get; }

        public string LinkUrl { get; }

        public string Title { get; }
    }
}
