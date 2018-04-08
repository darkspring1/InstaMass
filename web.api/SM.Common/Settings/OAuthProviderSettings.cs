using Microsoft.Extensions.Configuration;

namespace SM.Common.Settings
{
    public class OAuthProviderSettings
    {
        private readonly IConfiguration configuration;
        private readonly string prefix;

        public OAuthProviderSettings(IConfiguration configuration, string prefix)
        {
            this.configuration = configuration;
            this.prefix = prefix;
        }

        public string AppId => configuration[$"{prefix}:AppId"];
        public string AppSecret => configuration[$"{prefix}:AppSecret"];
    }
}