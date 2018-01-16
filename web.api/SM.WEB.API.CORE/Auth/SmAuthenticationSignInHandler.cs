using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SM.Common.Services;
using SM.Domain.Model;
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
            //Request.
            //throw new NotImplementedException();
        }

        public Task SignOutAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(AuthenticateResult.NoResult()); 
        }
    }
}
