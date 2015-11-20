using System;
using System.Collections.Generic;
using Simple.MemoryCache.Entities.Exceptions;

namespace Simple.MemoryCache.Entities
{
    public class CacheItemPolicy
    {
        public interface ICacheItemPolicyBuilder
        {
            CacheItemPolicyBuilder WithDirectoriesOrFilesChangeMonitor(IEnumerable<string> directoriesOrFilesPath);
            CacheItemPolicyBuilder WithAbsoluteExpiration(DateTimeOffset absoluteExpiration);
            CacheItemPolicyBuilder WithSlidingExpiration(TimeSpan slidingExpiration);
            CacheItemPolicyBuilder WithCacheItemPriority(CacheItemPriority cacheItemPriority);
            CacheItemPolicy Build();
        }

        private CacheItemPolicy() { }

        public IEnumerable<string> DirectoriesOrFiles { get; private set; }
        public DateTimeOffset? AbsoluteExpiration { get; private set; }
        public TimeSpan? SlidingExpiration { get; private set; }
        public CacheItemPriority CacheItemPriority { get; private set; }

        public class CacheItemPolicyBuilder : ICacheItemPolicyBuilder
        {
            private IEnumerable<string> DirectoriesOrFiles { get; set; }
            private DateTimeOffset? AbsoluteExpiration { get; set; }
            private TimeSpan? SlidingExpiration { get; set; }
            public CacheItemPriority CacheItemPriority { get; set; }
            
            public CacheItemPolicyBuilder WithDirectoriesOrFilesChangeMonitor(IEnumerable<string> directoriesOrFilesPath)
            {
                DirectoriesOrFiles = directoriesOrFilesPath;
                return this;
            }

            public CacheItemPolicyBuilder WithAbsoluteExpiration(DateTimeOffset absoluteExpiration)
            {
                AbsoluteExpiration = absoluteExpiration;
                return this;
            }

            public CacheItemPolicyBuilder WithSlidingExpiration(TimeSpan slidingExpiration)
            {
                SlidingExpiration = slidingExpiration;
                return this;
            }

            public CacheItemPolicyBuilder WithCacheItemPriority(CacheItemPriority cacheItemPriority)
            {
                CacheItemPriority = cacheItemPriority;
                return this;
            }

            public CacheItemPolicy Build()
            {
                if (AbsoluteExpiration != null && SlidingExpiration != null)
                { 
                    throw new InvalidStateException("both AbsoluteExpiration and SlidingExpiration were specified, one should use only one of them");
                }

                if (AbsoluteExpiration.HasValue && AbsoluteExpiration.Value.EqualsExact(DateTimeOffset.Now))
                { 
                    throw new InvalidStateException("AboluteExpiration has invalid value - equals NOW");
                }

                if (SlidingExpiration.HasValue && SlidingExpiration.Value.TotalMilliseconds == 0)
                { 
                    throw new InvalidStateException("SlidingExpiration has invalid value - equals zero");
                }

                return new CacheItemPolicy
                {
                    DirectoriesOrFiles = DirectoriesOrFiles,
                    AbsoluteExpiration = AbsoluteExpiration,
                    SlidingExpiration = SlidingExpiration,
                    CacheItemPriority = CacheItemPriority
                };
            }
        }
    }
}