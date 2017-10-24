namespace SM.Domain.Settings
{
    public class FBOAuthSettings : OAuthProviderSettings
    {
        protected override string ClientIdKey
        {
            get { return "fbClientId"; }
        }

        protected override string ClientSecretKey
        {
            get { return "fbClientSecret"; }
        }

        protected override string RedirectUriKey
        {
            get { return "fbRedirectUri"; }
        }

        public override string GetTokenUrl(string code)
        {
            return string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                ClientId, RedirectUri, ClientSecret, code);
        }
    }
}
