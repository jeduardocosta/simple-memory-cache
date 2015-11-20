using Simple.MemoryCache.Entities;

namespace Simple.MemoryCache.Converters
{
    public interface ICacheItemPriorityConverter
    {
        System.Web.Caching.CacheItemPriority Convert(CacheItemPriority cacheItemPriority);
    }
}