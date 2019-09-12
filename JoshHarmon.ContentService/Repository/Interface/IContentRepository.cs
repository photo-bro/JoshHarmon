using System.Collections.Generic;
using System.Threading.Tasks;
using JoshHarmon.ContentService.Models;

namespace JoshHarmon.ContentService.Repository.Interface
{
    public interface IContentRepository
    {
        Task<IEnumerable<PanelModel>> ReadAllPanelModels();
        Task<IEnumerable<ConnectModel>> ReadAllConnectModels();
        Task<IEnumerable<ProjectModel>> ReadAllProjectModels();
    }
}
