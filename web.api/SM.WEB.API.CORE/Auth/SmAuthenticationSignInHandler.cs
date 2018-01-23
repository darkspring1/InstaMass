using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SM.Common.Services;
using SM.Domain.Model;
using SM.WEB.Application.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SM.WEB.API.CORE
{
    public class SmAuthenticationSignInHandler : AuthenticationHandler<SmAuthenticationSignInOptions>,
        IAuthenticationSignInHandler
    {

        private readonly Func<UserService> _userServiceFunc;

        public SmAuthenticationSignInHandler(Func<UserService> userServiceFunc, IOptionsMonitor<SmAuthenticationSignInOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _userServiceFunc = userServiceFunc;
        }


        Task<ServiceResult<User>> CreateUserAsync(string email, string userName, string password)
        {
            return  _userServiceFunc().CreateAync(email, userName, password);
        }

        Task<ServiceResult<User>> GetUserAsync(string email)
        {
            return _userServiceFunc().FindByEmailAsync(email);
        }

        public async Task SignInAsync(ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            var email = principal.FindFirstValue(ClaimTypes.Email);
            ServiceResult<User> serviceResult;
            if (properties.Items.ContainsKey(SMClaimTypes.CreateNewUser))
            {
                var userName = principal.FindFirstValue(ClaimTypes.Name);
                var password = principal.FindFirstValue(SMClaimTypes.Password);
                serviceResult = await CreateUserAsync(email, userName, password);
            }
            else
            {
                serviceResult = await GetUserAsync(email);
            }

            if (serviceResult.IsFaulted)
            {
                Response.StatusCode = 400;
            }
            else
            {
                var user = serviceResult.Result;
                var claims = new Claim[]
                    {
                        new Claim(SMClaimTypes.UserId, user.Id.ToString())
                    };
                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                var authInfo = JsonConvert.SerializeObject(new { access_token = token });

                Response.Redirect(string.Format(Options.RedirectUri, authInfo));
            }

            

            
        }

        public Task SignOutAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(AuthenticateResult.NoResult()); 
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            return base.HandleChallengeAsync(properties);
        }
    }
}
