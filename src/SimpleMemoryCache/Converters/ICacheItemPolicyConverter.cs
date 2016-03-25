using SimpleMemoryCache.Entities;

namespace SimpleMemoryCache.Converters
{
    public interface ICacheItemPolicyConverter
    {
        System.Runtime.Caching.CacheItemPolicy Convert(CacheItemPolicy item);
    }
}