using Microsoft.Extensions.Configuration;

namespace SM.WEB.API.CORE.Settings
{
    public class SMSettings
    {
        private readonly IConfiguration _configuration;


        public SMSettings(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        ///<summary>
        ///ConnectionString
        ///</summary> 
        public string ConnectionString
        {
            get
            {
                return _configuration["ConnectionString"];
            }
        }

        public OAuthProviderSettings Facebook => new OAuthProviderSettings(_configuration, "Facebook");

        public JWTSettings JWT => new JWTSettings(_configuration, "JWT");


    }
}

