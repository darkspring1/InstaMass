using Microsoft.AspNetCore.Authentication;

namespace SM.WEB.API.CORE
{

    public class SmAuthenticationSignInOptions : AuthenticationSchemeOptions
    {
        public string RedirectUri { get; set; }
    }
}
