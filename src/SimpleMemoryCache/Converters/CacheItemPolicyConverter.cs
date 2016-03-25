using System.Linq;
using SimpleMemoryCache.Entities;

namespace SimpleMemoryCache.Converters
{
    public class CacheItemPolicyConverter : ICacheItemPolicyConverter
    {
        public System.Runtime.Caching.CacheItemPolicy Convert(CacheItemPolicy item)
        {
            var result = new System.Runtime.Caching.CacheItemPolicy();
            
            if (item.AbsoluteExpiration.HasValue)
            { 
                result.AbsoluteExpiration = item.AbsoluteExpiration.Value;
            }

            if (item.SlidingExpiration.HasValue)
            { 
                result.SlidingExpiration = item.SlidingExpiration.Value;
            }

            if (item.DirectoriesOrFiles != null && item.DirectoriesOrFiles.Any())
            { 
                result.ChangeMonitors.Add(new System.Runtime.Caching.HostFileChangeMonitor(item.DirectoriesOrFiles.ToList()));
            }

            return result;
        }
    }
}