using System.Threading.Tasks;

namespace SM.Domain.Services
{
    class FBAuthService : ExternalAuthService
    {
        //access_token=CAAJZCiw505FMBAFKLZCf3YeZC9iBbzr88rsG3t5g54ZBoq8fczPoqHZBFwYX1bmGl6V2RLov7zGWZBuQl0lw1ZBOwJT2L2kSRIHQE55vquvZAwkQpoDHBMelZAuBn0eGaZAOeGOw5JolmiAQle99bMZAkfIZA0JsgdSWMWcHM2Yz8AbZCNnBZBwaZCDM9QR3i7YFkLaZCcwCe8QJfxx4ZAAZDZD&expires=5105384

        public override async Task<ExternalUserInfo> GetUserAsync(string accessToken)
        {
            var meUrl = string.Format("https://graph.facebook.com/me?fields=id,first_name,last_name,email&access_token={0}", accessToken);
            var me = await SendRequestJsonAsync(meUrl);
            return new ExternalUserInfo(me["id"].ToString(), me["email"].ToString(), me["first_name"].ToString(), me["last_name"].ToString());
        }

       
    }
}
