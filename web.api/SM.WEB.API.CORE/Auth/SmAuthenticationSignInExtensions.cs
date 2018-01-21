using Microsoft.AspNetCore.Authentication;
using System;

namespace SM.WEB.API.CORE
{
    public static class SmAuthenticationSignInExtensions
    {
        public static AuthenticationBuilder AddSmSignIn(this AuthenticationBuilder builder)
            => builder.AddSmSignIn(SmAuthenticationSignInDefaults.AuthenticationScheme);

        public static AuthenticationBuilder AddSmSignIn(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddSmSignIn(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddSmSignIn(this AuthenticationBuilder builder, Action<SmAuthenticationSignInOptions> configureOptions)
            => builder.AddSmSignIn(SmAuthenticationSignInDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddSmSignIn(this AuthenticationBuilder builder, string authenticationScheme, Action<SmAuthenticationSignInOptions> configureOptions)
            => builder.AddSmSignIn(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddSmSignIn(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<SmAuthenticationSignInOptions> configureOptions)
        {
            return builder.AddScheme<SmAuthenticationSignInOptions, SmAuthenticationSignInHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
