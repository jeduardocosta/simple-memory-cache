using Autofac;
using SimpleMemoryCache.Converters;
using SimpleMemoryCache.Entities;

namespace SimpleMemoryCache.DI
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MemoryCacher>()
                .As<IMemoryCacher>();

            builder
                .RegisterType<CacheItemPolicyConverter>()
                .As<ICacheItemPolicyConverter>();

            builder
                .RegisterType<CacheItemPriorityConverter>()
                .As<ICacheItemPriorityConverter>();

            builder
                .RegisterType<CacheItemPolicy.CacheItemPolicyBuilder>()
                .As<CacheItemPolicy.ICacheItemPolicyBuilder>(); 
        }
    }
}