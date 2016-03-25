using System;
using SimpleMemoryCache.Entities;

namespace SimpleMemoryCache
{
    public interface IMemoryCacher
    {
        void Add<T>(string key, T value, TimeSpan expiration);
        void Add<T>(string key, T value, CacheItemPolicy policy);

        T Retrieve<T>(string key);
        T RetrieveOrElse<T>(string key, TimeSpan expiration, Func<T> retrievalDelegate);
        T RetrieveOrElse<T>(string key, CacheItemPolicy policy, Func<T> retrievalDelegate);

        void Set<T>(string key, T value, TimeSpan expiration);
        void Set<T>(string key, T value, CacheItemPolicy policy);
    }
}