using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.WEB.API.Ioc;
using SM.Domain.Events;
using StructureMap;
using Akka.Actor;
using SM.WEB.API.Akka;
using SM.TaskEngine.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

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
            /*
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                    {
                        o.RequireHttpsMetadata = false;
                        o.Authority = "dsdsd";
                        o.Audience = "audience";
                    })
                    .AddFacebook(o =>
                    {
                        o.AppId = "196483557554508";
                        o.AppSecret = "601ca0e2d0898e7105785db885bca4c9";
                    });
              */      
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = SmAuthenticationSignInDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                //.AddCookie(o => o.LoginPath = new PathString("/login"))
                .AddSignIn()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.Authority = "dsdsd";
                    o.Audience = "audience";
                })
                .AddFacebook(o =>
                {
                    o.AppId = "196483557554508";
                    o.AppSecret = "601ca0e2d0898e7105785db885bca4c9";
                });
                

            services
                .AddMvc()
                .AddFluentValidation()
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

            app.UseAuthentication();
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
