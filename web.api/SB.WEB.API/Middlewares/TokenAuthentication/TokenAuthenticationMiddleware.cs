using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using Sb.Api.Middlewares.TokenAuthentication;

namespace Sb.Api.Middlewares.Auth
{
    /// <summary>
    /// Для аторизации девайсов с бесконечными токенами 
    /// </summary>
    public class TokenAuthenticationMiddleware : AuthenticationMiddleware<TokenAuthenticationOptions>
    {
        
        public TokenAuthenticationMiddleware(OwinMiddleware next, TokenAuthenticationOptions options)
            : base(next, options)
        {
           
        }
        

        // Called for each request, to create a handler for each request.
        protected override AuthenticationHandler<TokenAuthenticationOptions> CreateHandler()
        {
            return new TokenAuthenticationHandler();
        }
    }
}