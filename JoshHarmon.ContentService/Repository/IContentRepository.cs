using System.Collections.Generic;
using JoshHarmon.ContentService.Models;

namespace JoshHarmon.ContentService.Repository
{
    public interface IContentRepository
    {
        IEnumerable<PanelModel> ReadAllPanelModels();
        IEnumerable<ConnectModel> ReadAllConnectModels();
        IEnumerable<ProjectModel> ReadAllProjectModels();
    }
}
