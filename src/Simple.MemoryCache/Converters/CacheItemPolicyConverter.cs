using System.Linq;
using Simple.MemoryCache.Entities;

namespace Simple.MemoryCache.Converters
{
    public interface ICacheItemPolicyConverter
    {
        System.Runtime.Caching.CacheItemPolicy Convert(CacheItemPolicy item);
    }

    public class CacheItemPolicyConverter : ICacheItemPolicyConverter
    {
        public System.Runtime.Caching.CacheItemPolicy Convert(CacheItemPolicy item)
        {
            var result = new System.Runtime.Caching.CacheItemPolicy();
            
            if (item.AbsoluteExpiration.HasValue)
                result.AbsoluteExpiration = item.AbsoluteExpiration.Value;
            
            if (item.SlidingExpiration.HasValue)
                result.SlidingExpiration = item.SlidingExpiration.Value;

            if (item.DirectoriesOrFiles != null && item.DirectoriesOrFiles.Any())
                result.ChangeMonitors.Add(new System.Runtime.Caching.HostFileChangeMonitor(item.DirectoriesOrFiles.ToList()));

            return result;
        }
    }
}