using Simple.MemoryCache.Entities;

namespace Simple.MemoryCache.Converters
{
    public interface ICacheItemPolicyConverter
    {
        System.Runtime.Caching.CacheItemPolicy Convert(CacheItemPolicy item);
    }
}