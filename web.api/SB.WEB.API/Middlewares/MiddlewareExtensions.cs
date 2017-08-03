using Owin;

namespace SM.WEB.API.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IAppBuilder UseLogger(this IAppBuilder app)
        {
            return app.Use<NLogMiddleware>();
        }
/*
        public static IAppBuilder UseTokenAuthentication(this IAppBuilder app, TokenAuthenticationOptions options)
        {
            return app.Use(typeof(TokenAuthenticationMiddleware), options);
        }*/
    }
}
