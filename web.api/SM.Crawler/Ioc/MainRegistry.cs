using SM.Common.Cache;
using SM.Common.Log;
using SM.Domain.Events;
using SM.Domain.Persistent;
using SM.Domain.Persistent.EF;
using StructureMap;

namespace SM.Crawler.Ioc
{
    public class MainRegistry : Registry
    {
        public MainRegistry(string connectionString)
        {
            For<IHandlerContainer>().Use<EmptyHandlerContainer>();
            For<IDomainEventDispatcher>().Use<EventDispatcher>();
            For<ILogger>().Use<NLogLogger>();

            For<IInstaApiFactory>().Use<InstaApiFactory>();
            For<Manager>().Use<Manager>();

            For<DataContext>()
                .Use<DataContext>()
                .Ctor<string>("connectionString").Is(connectionString);

            For<ICacheProvider>().Use<MemoryCacheProvider>().Singleton();

            For<IUnitOfWork>().Use<EfUnitOfWork>();
        }
    }
}
