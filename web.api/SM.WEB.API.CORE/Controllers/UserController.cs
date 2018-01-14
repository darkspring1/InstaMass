using AngularJSAuthentication.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SM.Common.Log;
using SM.Domain.Model;
using SM.WEB.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        // POST api/Account/Register
        //[AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult> Register(UserModel userModel)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            var result = await _userServiceFunc().CreateAync(userModel.UserName, userModel.UserName, userModel.Password);

            return ActionResult(result);
        }

        // GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        //[AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<ActionResult> GetExternalLogin(string provider, string error = null)
        {
            string redirectUri = string.Empty;

            if (error != null)
            {
                return BadRequest(Uri.EscapeDataString(error));
            }

            if (User == null || !User.Identity.IsAuthenticated)
            {
                var authenticationProperties = new AuthenticationProperties();
                authenticationProperties.RedirectUri = "/api/user/ExternalLogin";
                
                return new ChallengeResult(provider, authenticationProperties);
            }

            var redirectUriValidationResult = await ValidateClientAndRedirectUri(this.Request);

            if (!string.IsNullOrWhiteSpace(redirectUriValidationResult.Error))
            {
                return BadRequest(redirectUriValidationResult);
            }

            redirectUri = redirectUriValidationResult.RedirectUri;

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return BadRequest();
            }

            var findResult = await _userServiceFunc().FindAsync(provider.ToExternalAuthProviderType(), externalLogin.ProviderKey);

            if (!findResult.IsSuccess) {
                return BadRequest();
            }
            //IdentityUser user = await _repo.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));


            bool hasRegistered = findResult.Result != null;

            //bool hasRegistered = true;

            redirectUri = string.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
                                            redirectUri,
                                            externalLogin.ExternalAccessToken,
                                            externalLogin.LoginProvider,
                                            hasRegistered.ToString(),
                                            externalLogin.UserName);

            return Redirect(redirectUri);

        }

        #region Helpers


        private async Task<dynamic> ValidateClientAndRedirectUri(HttpRequest request)
        {

            Uri redirectUri;

            var redirectUriString = GetQueryString(Request, "redirect_uri");

            Func<string, string, dynamic> getResult = (err, uri) => new { Error = err, RedirectUri = uri };
            Func<string, dynamic> getErrorResult = err => getResult(err, null);

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                return getErrorResult("redirect_uri is required");
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri)
            {
                return getErrorResult("redirect_uri is invalid");
            }

            var clientId = GetQueryString(Request, "client_id");

            if (string.IsNullOrWhiteSpace(clientId))
            {
                return getErrorResult("client_Id is required");
            }

            //var client = _repo.FindClient(clientId);

            var app = await _applicationServiceFunc().GetByIdAsync(clientId);

            if (app == null)
            {
                return getErrorResult($"App_id '{clientId}' is not registered in the system.");
            }

            /*
            if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
            }
            */

            return getResult(null, redirectUri.AbsoluteUri);

        }

        private string GetQueryString(HttpRequest request, string key)
        {
            if (request.Query == null) return null;

            var match = request.Query.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;

            return match.Value;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }
            public string ExternalAccessToken { get; set; }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirst(ClaimTypes.Name).Value,
                    ExternalAccessToken = identity.FindFirst("ExternalAccessToken").Value,
                };
            }
        }

        #endregion
    }
}
