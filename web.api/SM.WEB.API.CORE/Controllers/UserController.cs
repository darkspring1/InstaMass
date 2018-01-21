using AngularJSAuthentication.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM.Common.Log;
using SM.Domain.Model;
using SM.WEB.API.CORE;
using SM.WEB.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SM.WEB.API.Controllers
{
    [Route(Routes.ApiUser)]
    public class UserController : BaseController
    {

        private readonly Func<ApplicationService> _applicationServiceFunc;
        private readonly Func<UserService> _userServiceFunc;

        public UserController(
            ILogger logger,
            Func<ApplicationService> applicationServiceFunc,
            Func<UserService> userServiceFunc) : base(logger)
        {
            _applicationServiceFunc = applicationServiceFunc;
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
                    new Claim(SMClaimTypes.Password, model.Password)
            };

            AuthenticationProperties properties = new AuthenticationProperties();
            if (createNewUser)
            {
                claims.Add(new Claim(ClaimTypes.Name, userName));
                properties.Items.Add(SMClaimTypes.CreateNewUser, "");
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
                    properties.Items.Add(SMClaimTypes.CreateNewUser, "");
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
    }
}
