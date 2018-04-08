using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

namespace SM.Common.Settings
{
    public class JWTSettings
    {
        private readonly IConfiguration configuration;
        private readonly string prefix;

        public JWTSettings(IConfiguration configuration, string prefix)
        {
            this.configuration = configuration;
            this.prefix = prefix;
        }
        
        string Key => configuration[$"{prefix}:Key"];

        public double AccessTokenLifeTime
        {
            get
            {
                return double.Parse(configuration[$"{prefix}:AccessTokenLifeTime"], CultureInfo.InvariantCulture);
            }
        }

        public double RefreshTokenLifeTime
        {
            get
            {
                return double.Parse(configuration[$"{prefix}:RefreshTokenLifeTime"], CultureInfo.InvariantCulture);
            }
        }

        public string Audience => configuration[$"{prefix}:Audience"];

        public string Issuer => configuration[$"{prefix}:Issuer"];


        public  SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}