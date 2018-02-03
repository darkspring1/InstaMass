using Microsoft.AspNetCore.Authorization;

namespace SM.WEB.API.CORE.Auth
{
    public class Policies
    {
        public const string RefreshToken = "RefreshToken";
        public const string AccessToken = "AccessToken";

        public static void Init(AuthorizationOptions options)
        {
            options.AddPolicy(RefreshToken, builder =>
            {
                builder.RequireClaim(Claims.UserId);
                builder.RequireClaim(Claims.Refresh);
            });

            options.AddPolicy(AccessToken, builder =>
            {
                builder.RequireClaim(Claims.UserId);
                builder.RequireClaim(Claims.Access);
            });
        }
    }
}
