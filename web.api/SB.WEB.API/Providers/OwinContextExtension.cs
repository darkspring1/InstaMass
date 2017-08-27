using Microsoft.Owin;
using SM.Domain.Model;

namespace SM.WEB.API.Providers
{
    static class OwinContextExtension
    {

        public static ExternalAuthProviderType GetProvider(this IOwinContext ctx)
        {
            return ctx.Get<ExternalAuthProviderType>("as:provider");
        }

        public static void SetProvider(this IOwinContext ctx, ExternalAuthProviderType provider)
        {
            ctx.Set("as:provider", provider);
        }

        public static void SetExternalToken(this IOwinContext ctx, string value)
        {
            ctx.Set("as:external_token", value);
        }

        public static string GetExternalToken(this IOwinContext ctx)
        {
            return ctx.Get<string>("as:external_token");
        }

        public static bool IsExternalToken(this IOwinContext ctx)
        {
            return !string.IsNullOrEmpty(ctx.GetExternalToken());
        }

        public static void SetExternalUserId(this IOwinContext ctx, string value)
        {
            ctx.Set("as:external_user_id", value);
        }

        public static string GetExternalUserId(this IOwinContext ctx)
        {
            return ctx.Get<string>("as:external_user_id");
        }

    }
}
