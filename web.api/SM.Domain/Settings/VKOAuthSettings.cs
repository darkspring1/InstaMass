namespace SM.Domain.Settings
{
    public class VKOAuthSettings : OAuthProviderSettings
    {
        protected override string ClientIdKey
        {
            get { return "vkClientId"; }
        }

        protected override string ClientSecretKey
        {
            get { return "vkClientSecret"; }
        }

        protected override string RedirectUriKey
        {
            get { return "vkRedirectUri"; }
        }

        public override string GetTokenUrl(string code)
        {
            return string.Format(
                "https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&redirect_uri={2}&code={3}",
                ClientId, ClientSecret, RedirectUri, code);

        }
    }
}
