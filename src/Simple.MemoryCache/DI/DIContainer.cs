using Autofac;

namespace Simple.MemoryCache.DI
{
    public class DIContainer
    {
        private static IContainer _container;

        private static IContainer Container
        {
            get
            {
                if (_container == null) Setup();
                return _container;
            }
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        private static void Setup()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule(new MainModule());

            _container = builder.Build();
        }
    }
}