using Microsoft.Owin.Security;
using StructureMap;

namespace Sb.Api.Middlewares.Auth
{

    public class AuthTypes
    {
        public const string Token = "Token";
        public const string  LTT = "LongTimeToken";
    }

    public class TokenAuthenticationOptions : AuthenticationOptions
    {
        readonly string _tokenHeader;
        public Container Container { get; private set; }

        public TokenAuthenticationOptions(string tokenHeader, Container container, string type)
            : base(type)
        {
            Description.Caption = type;
            AuthenticationMode = AuthenticationMode.Active;
            _tokenHeader = tokenHeader;
            Container = container;
        }
    }
}