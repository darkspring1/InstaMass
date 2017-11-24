using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SM.WEB.API.Ioc;
using SM.Domain.Events;
using StructureMap;
using Akka.Actor;
using SM.WEB.API.Akka;
using SM.TaskEngine.Common;

namespace SM.WEB.API.CORE
{
    public class Startup
    {

        public static ActorSystem ActorSystem { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddControllersAsServices();

            return ConfigureIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var apiRegistry = new ApiRegistry();
            var container = new Container(apiRegistry);

            ActorSystem = AkkaConfig.CreateActorSystem();
            SystemActors systemActors = new SystemActors(ActorSystem, container);

            var domainEventHandlersRegistry = new DomainEventHandlersRegistry(systemActors.TaskApi);
            domainEventHandlersRegistry.IncludeRegistry(apiRegistry);
            var domainEventHandlersContainer = new Container(domainEventHandlersRegistry);

            container.Configure(x => {
                x.For<IHandlerContainer>()
                .Use<StructureMapEventHandlerContainer>()
               .Ctor<IContainer>().Is(domainEventHandlersContainer);
                x.Populate(services);

            });

            return container.GetInstance<IServiceProvider>();

        }

        private void OnShutdown()
        {
            // Do your cleanup here
            ActorSystem.Terminate();
        }
    }
}
