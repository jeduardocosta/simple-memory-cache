using SimpleMemoryCache.Entities;

namespace SimpleMemoryCache.Converters
{
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