using Microsoft.AspNetCore.Authorization;

namespace SM.WEB.API.CORE.Auth
{
    public class SmAuthorizeAttribute : AuthorizeAttribute
    {
        public SmAuthorizeAttribute(string policy = Policies.AccessToken) : base(policy)
        {
        }
    }
}
