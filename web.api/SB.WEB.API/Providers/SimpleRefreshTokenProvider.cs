using Microsoft.Owin.Security.Infrastructure;
using SM.WEB.Application.Services;
using StructureMap;
using System;
using System.Threading.Tasks;

namespace SM.WEB.API.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly Container _container;

        public SimpleRefreshTokenProvider(Container container)
        {
            _container = container;
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");


            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = SM.Domain.Model.RefreshToken.Create(refreshTokenId, context.Ticket.Identity.Name, clientid, Convert.ToDouble(refreshTokenLifeTime));


            context.Ticket.Properties.IssuedUtc = token.IssuedAt;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresAt;

            token.SetProtectedTicket(context.SerializeTicket());
            using (var nested = _container.GetNestedContainer())
            {
                var result = await nested.GetInstance<AuthService>().AddRefreshTokenAsync(token);
                if (result.IsSuccess)
                {
                    context.SetToken(refreshTokenId);
                }
            }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Domain.Model.RefreshToken.GetHash(context.Token);

            using (var nested = _container.GetNestedContainer())
            {
                var _repo = nested.GetInstance<AuthService>();
                var refreshToken = await _repo.FindRefreshTokenAsync(hashedTokenId);

                if (refreshToken.IsSuccessAndNotNullResult)
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.Result.ProtectedTicket);
                    var result = await _repo.RemoveRefreshTokenAsync(hashedTokenId);
                }
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}