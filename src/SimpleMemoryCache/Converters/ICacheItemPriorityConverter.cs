using SimpleMemoryCache.Entities;

namespace SimpleMemoryCache.Converters
{
    public interface ICacheItemPriorityConverter
    {
        System.Web.Caching.CacheItemPriority Convert(CacheItemPriority cacheItemPriority);
    }
}