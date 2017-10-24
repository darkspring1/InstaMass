using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domain.Settings
{
    public abstract class OAuthProviderSettings
    {
        protected abstract string ClientIdKey { get; }
        protected abstract string ClientSecretKey { get; }
        protected abstract string RedirectUriKey { get; }

        string _clientId;
        string _clientSecret;
        string _redirectUri;


        public abstract string GetTokenUrl(string code);

        public string ClientId
        {
            get
            {
                if (_clientId == null)
                {
                    _clientId = ConfigurationManager.AppSettings[ClientIdKey];
                }
                return _clientId;
            }
        }

        [JsonIgnore]
        /// <summary>
        /// !!!НЕ ДОЛЖЕН ПОПАДАТЬ НА КЛИЕНТА
        /// </summary>
        public string ClientSecret
        {
            get
            {
                if (_clientSecret == null)
                {
                    _clientSecret = ConfigurationManager.AppSettings[ClientSecretKey];
                }
                return _clientSecret;
            }
        }

        public string RedirectUri
        {
            get
            {
                if (_redirectUri == null)
                {
                    _redirectUri = ConfigurationManager.AppSettings[RedirectUriKey];
                }
                return _redirectUri;
            }
        }
    }
}
