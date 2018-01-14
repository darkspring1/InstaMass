using Microsoft.AspNetCore.Authentication;
using System;

namespace SM.WEB.API.CORE
{
    public static class SmAuthenticationSignInExtensions
    {
        public static AuthenticationBuilder AddSignIn(this AuthenticationBuilder builder)
            => builder.AddSignIn(SmAuthenticationSignInDefaults.AuthenticationScheme);

        public static AuthenticationBuilder AddSignIn(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddSignIn(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddSignIn(this AuthenticationBuilder builder, Action<SmAuthenticationSignInOptions> configureOptions)
            => builder.AddSignIn(SmAuthenticationSignInDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddSignIn(this AuthenticationBuilder builder, string authenticationScheme, Action<SmAuthenticationSignInOptions> configureOptions)
            => builder.AddSignIn(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddSignIn(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<SmAuthenticationSignInOptions> configureOptions)
        {
            return builder.AddScheme<SmAuthenticationSignInOptions, SmAuthenticationSignInHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
