using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SM.Domain.Model;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SM.Domain.Services
{

    public class ExternalUserInfo
    {
        public string Email { get; }

        public string UserId { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public ExternalAuthProviderType ProviderType { get; }

        public ExternalUserInfo(string userId, string email, string firstName, string lastName, ExternalAuthProviderType providerType)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = LastName;
            ProviderType = providerType;
        }
    }


    public abstract class ExternalAuthService
    {
        protected async Task<Stream> SendRequestAsync(string url)
        {
            WebRequest req = WebRequest.Create(url);
            req.Method = "GET";
            req.Timeout = 30000;
            WebResponse response = await req.GetResponseAsync();
            return response.GetResponseStream();
        }

        protected async Task<JObject> SendRequestJsonAsync(string url)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(await SendRequestAsync(url)))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return (JObject)serializer.Deserialize(jsonTextReader);
            }
        }

        public abstract Task<ExternalUserInfo> GetUserAsync(string accessToken);

    }
}
