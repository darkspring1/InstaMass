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
                var claims = new Claim[] { new Claim(SMClaimTypes.UserId, user.Id.ToString()) };
                var now = DateTime.UtcNow;
                var key = AuthOptions.GetSymmetricSecurityKey();
                var algorithm = SecurityAlgorithms.HmacSha256;
                // создаем JWT-токен
                var accessToken = new JwtSecurityToken(
                        issuer: AuthOptions.Issuer,
                        audience: AuthOptions.Audience,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.AccessTokenLifeTime)),
                        signingCredentials: new SigningCredentials(key, algorithm));

                var refreshToken = new JwtSecurityToken(
                        issuer: AuthOptions.Issuer,
                        audience: AuthOptions.Audience,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.RefreshTokenLifeTime)),
                        signingCredentials: new SigningCredentials(key, algorithm));


                var accessTokenWriten = new JwtSecurityTokenHandler().WriteToken(accessToken);
                var refreshTokenWriten = new JwtSecurityTokenHandler().WriteToken(refreshToken);

                var authInfo = JsonConvert.SerializeObject(new { access_token = accessTokenWriten, refresh_token = refreshTokenWriten });

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
