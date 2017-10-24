using System;
using Newtonsoft.Json;

namespace SM.Domain.Services
{
    /*
    public class VKOAuthProvider : OAuthProvider
    {


        public VKOAuthProvider()
            : base(new Domain.Settings.VKOAuthSettings())
        { }

        public override ExternalUserInfo GetUser(string accessToken)
        {
            var tokenInfoStr = RequestToken(code);
            var tokenInfo = JsonConvert.DeserializeObject<VKTokenInfo>(tokenInfoStr);

            var response = SendRequestJson(string.Format("https://api.vk.com/method/users.get?user_id={0}&v=5.8&fields=photo_50,first_name,last_name", tokenInfo.UserId));
            var user = response["response"][0];
            tokenInfo.AvatarUrl = user["photo_50"].ToString();
            tokenInfo.FirstName = user["first_name"].ToString();
            tokenInfo.LastName = user["last_name"].ToString();
            return tokenInfo;
        }

        public override ExternalUserInfo GetUser(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
    */
}
