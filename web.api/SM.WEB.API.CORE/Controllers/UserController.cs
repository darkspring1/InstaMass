using AngularJSAuthentication.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SM.Common.Log;
using SM.Common.Settings;
using SM.WEB.API.CORE;
using SM.WEB.API.CORE.Auth;
using SM.WEB.Application.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SM.WEB.API.Controllers
{
    [Route(Routes.ApiUser)]
    public class UserController : BaseController
    {
        private readonly Func<UserService> _userServiceFunc;

        readonly SMSettings _settings;

        public UserController(
            ILogger logger,
            SMSettings settings,
            Func<UserService> userServiceFunc) : base(logger)
        {
            _settings = settings;
            _userServiceFunc = userServiceFunc;
        }

        ActionResult SignInPrivate(SignInModel model, string userName, bool createNewUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Claim> claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(Claims.Password, model.Password)
            };

            AuthenticationProperties properties = new AuthenticationProperties();
            if (createNewUser)
            {
                claims.Add(new Claim(ClaimTypes.Name, userName));
                properties.Items.Add(Claims.CreateNewUser, "");
            }

            return SignIn(new ClaimsPrincipal(new ClaimsIdentity(claims)), properties, SmAuthenticationSignInDefaults.AuthenticationScheme);
        }

        ActionResult ChallengeResultPrivate(string provider, bool createNewUser)
        {
            if (User == null || !User.Identity.IsAuthenticated)
            {
                var properties = new AuthenticationProperties();
                if (createNewUser)
                {
                    properties.Items.Add(Claims.CreateNewUser, "");
                }
                //properties.RedirectUri = "http://localhost:8080/authcomplete.html";
                return new ChallengeResult(provider, properties);
            }

            return BadRequest();
        }
        
        [HttpPost]
        [Route(Routes.Login)]
        public ActionResult Login(SignInModel model)
        {
            return SignInPrivate(model, null, false);
        }

        [HttpPost]
        [Route(Routes.Register)]
        public ActionResult Register(SignUpModel model)
        {
            return SignInPrivate(model, model.UserName, true);
        }

        [HttpGet]
        [Route(Routes.LoginExternal)]
        public ActionResult LoginExternal(string provider)
        {
            return ChallengeResultPrivate(provider, false);
        }

        [HttpPost]
        [Route(Routes.RegisterExternal)]
        public ActionResult RegisterExternal(string provider)
        {
            return ChallengeResultPrivate(provider, true);
        }

        [SmAuthorize(policy: Policies.RefreshToken)]
        [HttpPost]
        [Route(Routes.TokenRefresh)]
        public Task<ActionResult> TokenRefresh()
        {
            var tokenHeaderValue = this.Request.Headers["Authorization"];
            string oldToken = tokenHeaderValue[0].Substring(7);
            var result =  _userServiceFunc().RefreshTokenAsync(
                UserId,
                oldToken,
                _settings.JWT.AccessTokenLifeTime,
                _settings.JWT.RefreshTokenLifeTime,
                _settings.JWT.Issuer,
                _settings.JWT.Audience,
                _settings.JWT.GetSymmetricSecurityKey()
                );

            return ActionResultAsync(result);
        }
    }
}
