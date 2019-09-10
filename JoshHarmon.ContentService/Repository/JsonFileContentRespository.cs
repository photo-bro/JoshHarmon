using System;
using System.Collections.Generic;
using System.IO;
using JoshHarmon.ContentService.Models;
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

        public IEnumerable<PanelModel> ReadAllPanelModels() => Content?.Panels;

        public IEnumerable<ConnectModel> ReadAllConnectModels() => Content?.Connections;

        public IEnumerable<ProjectModel> ReadAllProjectModels() => Content?.Projects;
    }
}
