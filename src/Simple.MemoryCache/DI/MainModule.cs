using Autofac;
using Simple.MemoryCache.Converters;
using Simple.MemoryCache.Entities;

namespace Simple.MemoryCache.DI
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