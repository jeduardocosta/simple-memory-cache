using Simple.MemoryCache.Entities;

namespace Simple.MemoryCache.Converters
{
    public interface ICacheItemPriorityConverter
    {
        System.Web.Caching.CacheItemPriority Convert(CacheItemPriority cacheItemPriority);
    }

    public class CacheItemPriorityConverter : ICacheItemPriorityConverter
    {
        public System.Web.Caching.CacheItemPriority Convert(CacheItemPriority cacheItemPriority)
        {
            return cacheItemPriority == CacheItemPriority.Default 
                    ? System.Web.Caching.CacheItemPriority.Default
                    : (System.Web.Caching.CacheItemPriority)cacheItemPriority;
        }
    }
}