using SM.Common.Cache;
using SM.Common.Log;
using SM.Domain.Events;
using SM.Domain.Persistent;
using SM.Domain.Persistent.EF;
using StructureMap;

namespace SM.WEB.API.Ioc
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry(string connectionString)
        {
            For<IDomainEventDispatcher>().Use<EventDispatcher>();
            /*
            IncludeRegistry<MappingEngineRegistry>();

            For<DataContext>().Use<DataContext>().Ctor<string>("connectionString").Is("safetyButton");
            For<IRepositoryProvider>().Use<RepositoryProvider>();
            */
            For<ILogger>().Use<NLogLogger>();

            For<DataContext>()
                .Use<DataContext>()
                .Ctor<string>("connectionString").Is(connectionString);

            For<ICacheProvider>().Use<MemoryCacheProvider>().Singleton();

            For<IUnitOfWork>().Use<EfUnitOfWork>();
        }
    }
}
