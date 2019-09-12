using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JoshHarmon.ContentService.Models;
using JoshHarmon.ContentService.Repository.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JoshHarmon.ContentService.Repository
{
    public class JsonFileContentRespository : IContentRepository
    {
        private readonly string _fileName;
        private readonly ILogger<JsonFileContentRespository> _logger;

        private string FileString => File.ReadAllText(_fileName);

        private ContentModel Content
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<ContentModel>(FileString);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error deserializing json content at '{FilePath}'.", _fileName);
                    return new ContentModel();
                }
            }
        }

        public JsonFileContentRespository(string fileName, ILogger<JsonFileContentRespository> logger)
        {
            _fileName = fileName;
            _logger = logger;
        }

        public virtual async Task<IEnumerable<PanelModel>> ReadAllPanelModels()
            => await Task.FromResult(Content?.Panels);

        public virtual async Task<IEnumerable<ConnectModel>> ReadAllConnectModels()
            => await Task.FromResult(Content?.Connections);

        public virtual async Task<IEnumerable<ProjectModel>> ReadAllProjectModels()
            => await Task.FromResult(Content?.Projects);
    }
}
