using SM.Common.Cache;
using SM.Common.Log;
using SM.Domain.Events;
using SM.Domain.Persistent;
using SM.Domain.Persistent.EF;
using StructureMap;
using System.Configuration;

namespace SM.WEB.API.Ioc
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry()
        {
            For<IDomainEventDispatcher>().Use<EventDispatcher>();
            /*
            IncludeRegistry<MappingEngineRegistry>();

            For<DataContext>().Use<DataContext>().Ctor<string>("connectionString").Is("safetyButton");
            For<IRepositoryProvider>().Use<RepositoryProvider>();
            */
            For<ILogger>().Use<NLogLogger>();

            //For(typeof(Business.Dal.IRepository<>)).Use(typeof(EFRepository<>));

            For<IEntityFrameworkDataContext>()
                .Use<EFDataContext>()
                .Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["SM"].ConnectionString);

            For<ICacheProvider>().Use<MemoryCacheProvider>().Singleton();

            
            For<IUnitOfWork>().Use<EfUnitOfWork>();
        }
    }
}
