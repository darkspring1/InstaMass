using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Owin;
using Microsoft.Owin;
using System.Web.Http.Dispatcher;
using StructureMap;
using sm.web.api;
using SM.WEB.API.Middlewares;
using SM.WEB.API.Ioc;

namespace SM.WEB.API
{
    class App
    {
        //private static sb.core.log.ILogger _logger = new NLogLogger();

        public static Container Container = new Container(new ApiRegistry());

        public void Configuration(IAppBuilder app)
        {
            //_logger.Debug("Start configuration");

            var config = new HttpConfiguration();

            EnableCrossSiteRequests(app, config);
            
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());
            config.Services.Replace(typeof(IHttpControllerActivator), new StructureMapWebApiControllerActivator(Container));


            //SwaggerConfig(config);
            
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            app.UseLogger();
           
            //AuthConfiguration(app);
            
            app
                .UseWebApi(config)
                .Use((c, next) =>
                {
                    c.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Task.FromResult(0);
                });
                

            //_logger.Debug("Configuration finished");
        }


        void EnableCrossSiteRequests(IAppBuilder app, HttpConfiguration config)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //var cors = new EnableCorsAttribute(
            //    origins: "*",
            //    headers: "*",
            //    methods: "*");
            //config.EnableCors(cors);
        }

        /*
        private void AuthConfiguration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(AuthTypes.Token);
            var tokenOptions = new TokenAuthenticationOptions("auth-token", Container, AuthTypes.Token);
            app.UseTokenAuthentication(tokenOptions);
        }
        */
     
    }
}
