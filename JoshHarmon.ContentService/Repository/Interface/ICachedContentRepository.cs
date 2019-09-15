using System;
using JoshHarmon.Cache.Cached.Interface;

namespace JoshHarmon.ContentService.Repository.Interface
{
    public interface ICachedContentRepository : IContentRepository, ICached
    {
    }
}
