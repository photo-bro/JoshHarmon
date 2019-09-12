using System;
namespace JoshHarmon.Cache.Interface
{
    public interface ICacheConfig
    {
        TimeSpan DefaultExpirationDuration { get; set; }
        bool UseUtc { get; set; }
    }
}
