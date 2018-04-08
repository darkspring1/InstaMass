using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SM.Common.Services;
using SM.Common.Settings;
using SM.WEB.Application.DTO;
using SM.WEB.Application.Services;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SM.WEB.API.CORE
{
    public class SmAuthenticationSignInHandler : AuthenticationHandler<SmAuthenticationSignInOptions>,
        IAuthenticationSignInHandler
    {
        private readonly SMSettings _settings;
        private readonly UserService _userService;

        public SmAuthenticationSignInHandler(
            SMSettings settings,
            UserService userServiceFunc,
            IOptionsMonitor<SmAuthenticationSignInOptions> options,
            ILoggerFactory logger,
            UrlEncoder
            encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this._settings = settings;
            _userService = userServiceFunc;
        }

        public async Task SignInAsync(ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            var email = principal.FindFirstValue(ClaimTypes.Email);
            ServiceResult<TokenData> serviceResult;
            if (properties.Items.ContainsKey(Claims.CreateNewUser))
            {
                var userName = principal.FindFirstValue(ClaimTypes.Name);
                var password = principal.FindFirstValue(Claims.Password);
                serviceResult = await _userService.CreateNewUserAync(
                    email,
                    userName,
                    password,
                    _settings.JWT.AccessTokenLifeTime,
                    _settings.JWT.RefreshTokenLifeTime,
                    _settings.JWT.Issuer,
                    _settings.JWT.Audience,
                    _settings.JWT.GetSymmetricSecurityKey()
                    );
            }
            else
            {
                serviceResult = await _userService.FindByEmailAsync(
                    email,
                    _settings.JWT.AccessTokenLifeTime,
                    _settings.JWT.RefreshTokenLifeTime,
                    _settings.JWT.Issuer,
                    _settings.JWT.Audience,
                    _settings.JWT.GetSymmetricSecurityKey()
                    );
            }

            if (serviceResult.IsFaulted)
            {
                Response.StatusCode = 400;
            }
            else
            {
                var tokenData = serviceResult.Result;
                var authInfo = JsonConvert.SerializeObject(tokenData, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
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
