using System;
using JoshHarmon.Cache.Interface;

namespace JoshHarmon.Cache
{
    public class CacheConfig : ICacheConfig
    {
       public TimeSpan DefaultExpirationDuration { get; set; }
       public bool UseUtc { get; set; }
    }
}
