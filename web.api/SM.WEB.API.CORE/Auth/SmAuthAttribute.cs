using Microsoft.AspNetCore.Authorization;

namespace SM.WEB.API.CORE.Auth
{
    public class SmAuthAttribute : AuthorizeAttribute
    {
        public SmAuthAttribute(string policy = Policies.AccessToken) : base(policy)
        {
        }
    }
}
