using AngularJSAuthentication.API;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SM.Common.Services;
using SM.Domain.Model;
using SM.WEB.Application.Services;
using StructureMap;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SM.WEB.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IContainer _container;
        public SimpleAuthorizationServerProvider(IContainer container)
        {
            _container = container;
        }

        public async Task<bool> ValidateExternal(OAuthValidateClientAuthenticationContext context)
        {
            string provider = context.Parameters["provider"];
            string externalAccessToken = context.Parameters["external_token"];

            if (string.IsNullOrWhiteSpace(provider) )
            {
                context.SetError("invalid_provider", "Provider should be sent.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(externalAccessToken))
            {
                context.SetError("invalid_external_access_token", "external_token should be sent.");
                return false;
            }

            using (var nested  = _container.GetNestedContainer())
            {
                var userService = nested.GetInstance<UserService>();
                var providerType = provider.ToExternalAuthProviderType();


                var externalUserInfoResult = await userService.GetExternalUserInfoAsync(providerType, externalAccessToken);

                if (externalUserInfoResult.IsFaultedOrNullResult)
                {
                    return false;
                }

                var findResult = await userService.FindAsync(providerType, externalUserInfoResult.Result.UserId);

                if (findResult.IsFaultedOrNullResult)
                {
                    return false;
                }

                context.OwinContext.SetProvider(providerType);
                context.OwinContext.SetExternalToken(externalAccessToken);
                context.OwinContext.SetExternalUserId(externalUserInfoResult.Result.UserId);
            }

            return true;

        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            ServiceResult<Domain.Model.Application> aplicationServiceresult = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return;
            }


            using (var nested = _container.GetNestedContainer())
            {
                var appService = nested.GetInstance<ApplicationService>();
                aplicationServiceresult = await appService.GetByIdAsync(context.ClientId);
            }

           

            if (aplicationServiceresult.IsFaultedOrNullResult)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return;
            }

            if (aplicationServiceresult.Result.Type == SM.Domain.Model.ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return;
                }
                else
                {
                    if (aplicationServiceresult.Result.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return;
                    }
                }
            }

            if (!aplicationServiceresult.Result.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(context.Parameters["provider"]))
            {
                var externalValidate = await ValidateExternal(context);
                if (!externalValidate)
                {
                    return;
                }
            }
            context.OwinContext.Set("as:clientAllowedOrigin", aplicationServiceresult.Result.AllowedOrigin);
            context.OwinContext.Set("as:clientRefreshTokenLifeTime", aplicationServiceresult.Result.RefreshTokenLifeTime.ToString());
            context.Validated();
        }

   

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            ServiceResult<User> userResult;
            using (var nested = _container.GetNestedContainer())
            {
                var appService = nested.GetInstance<UserService>();
                userResult = await appService.FindAsync(context.UserName, context.Password);
                if (context.OwinContext.IsExternalToken())
                {
                    userResult = await appService.FindAsync(context.OwinContext.GetProvider(), context.OwinContext.GetExternalUserId());
                }
                else
                {
                    userResult = await appService.FindAsync(context.UserName, context.Password);
                }
                
                if (userResult.IsFaultedOrNullResult)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var email = userResult.Result.Email.ToString();
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Email, email));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim("sub", email));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    { 
                        "userName", email
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            
            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

       


    }
}