using StructureMap.Configuration.DSL;

namespace SM.WEB.API.Ioc
{
    public class MappingEngineRegistry : Registry
    {
        /*
        public MappingEngineRegistry()
        {
            var profiles =
                typeof(DeviceMappingProfile)
                .Assembly
                .GetTypes()
                .Where(t => typeof(Profile).IsAssignableFrom(t))
                .Select(t => (Profile)Activator.CreateInstance(t)).ToList();


            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    //Trace.TraceInformation($"Добавляем в конфигурацию маппера профиль: {profile.GetType().Name}");
                    cfg.AddProfile(profile);
                }
            });

            For<MapperConfiguration>().Use(config).Singleton();
            For<IMapper>().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));
        }*/
    }
}
