using StructureMap.Configuration.DSL;

namespace SM.WEB.API.Ioc
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry()
        {/*
            IncludeRegistry<MappingEngineRegistry>();

            For<DataContext>().Use<DataContext>().Ctor<string>("connectionString").Is("safetyButton");
            For<IRepositoryProvider>().Use<RepositoryProvider>();

            For< sb.core.log.ILogger>().Use<NLogLogger>();

            For(typeof(Business.Dal.IRepository<>)).Use(typeof(EFRepository<>));

            For<IEntityFrameworkDataContext>().Use<EFDataContext>().Ctor<string>("connectionString").Is("safetyButton");

            For<ICacheProvider>().Use<MemoryCacheProvider>().Singleton();

            For<ISocialNetworkService>().Use<SocialNetworkService>();

            For<IUnitOfWork>().Use<EfUnitOfWork>();*/
        }
    }
}
