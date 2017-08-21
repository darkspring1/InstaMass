using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Newtonsoft.Json;
using Owin;
using Microsoft.Owin;
using System.Web.Http.Dispatcher;
using StructureMap;
using SM.web.api;
using SM.WEB.API.Middlewares;
using SM.WEB.API.Ioc;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Data.Entity;
using AngularJSAuthentication.API;
using SM.WEB.API.Providers;
using Microsoft.Owin.Security.Facebook;
using AngularJSAuthentication.API.Providers;
using Microsoft.Owin.Security.Google;

namespace SM.WEB.API
{
    class App
    {
        //private static sb.core.log.ILogger _logger = new NLogLogger();

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static Container Container = new Container(new ApiRegistry());
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

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
            ConfigureOAuth(app);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, AngularJSAuthentication.API.Migrations.Configuration>());

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

        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);


            /*
            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "xxxxxx",
                ClientSecret = "xxxxxx",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);
            */
            
            //Configure Facebook External Login
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "196483557554508",
                AppSecret = "601ca0e2d0898e7105785db885bca4c9",
                Provider = new FacebookAuthProvider()
            };
            facebookAuthOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookAuthOptions);
            

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
