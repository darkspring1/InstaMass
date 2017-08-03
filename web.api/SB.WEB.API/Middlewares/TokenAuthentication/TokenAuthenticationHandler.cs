using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Sb.Api.Middlewares.Auth;
using Sb.Business.Entities;
using Sb.Business.Services;
using StructureMap;

namespace Sb.Api.Middlewares.TokenAuthentication
{
    public class TokenAuthenticationHandler
        : AuthenticationHandler<TokenAuthenticationOptions>
    {
        Task<AuthenticationTicket> EmptyTicket()
        {
            return Task.FromResult(new AuthenticationTicket(null, null));
        }
        
        /// <summary>
        /// для тесторования
        /// </summary>
        protected virtual Microsoft.Owin.IOwinRequest OwinRequest => Request;

        protected virtual Container Container => Options.Container;

        protected virtual string AuthenticationType => Options.AuthenticationType;

        string GetToken()
        {
            var paramName = "auth-token";
            var token = OwinRequest.Headers[paramName];
            if (string.IsNullOrEmpty(token))
            {
                /*пробуем найти токен в урле*/
                token = OwinRequest.Query.Get(paramName);
            }
            return token;
        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return EmptyTicket();
            }

            ClaimsIdentity identity = null;
            using (var container = Container.GetNestedContainer())
            {
                var userService = container.GetInstance<UserService>();
                var serviceResult = userService.GetByToken(token);
                if (serviceResult.IsSuccess && serviceResult != null)
                {
                    var user = serviceResult.Result;
                    OwinRequest.Context.Set("sb.user", user);
                    identity = new ClaimsIdentity(AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    foreach (var role in user.Roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }

                    foreach (var grant in user.Grants)
                    {
                        identity.AddClaim(new Claim("Grant", grant));
                    }
                }
            }

            if (identity == null)
            {
                return EmptyTicket();
            }

            return Task.FromResult(new AuthenticationTicket(identity, null));
        }
    }
}
