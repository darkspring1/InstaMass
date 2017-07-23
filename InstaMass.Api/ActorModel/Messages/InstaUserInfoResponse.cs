using InstaMass;
using System.Collections.Generic;

namespace Api.ActorModel.Messages
{

    public class InstaUserInfoResponse
    {
        public  IEnumerable<InstaUserInfo> Users { get; }

        public InstaUserInfoResponse(IEnumerable<InstaUserInfo> users)
        {
            Users = users;
        }
    }


    class InstaUserRepository
    {

        public static InstaUserInfo[] Users
        {
            get
            {
                return new[] { new InstaUserInfo("vasin5462", "supervasia", new[] { "tree" }, new InstaUserActionType[] { InstaUserActionType.LikeByTag }) };
            }
        }
    }

    public class InstaUserInfo
    {
        public string Login { get; }

        public string Password { get; }

        public string[] Tags { get; }

        public InstaUserActionType[] Actions { get; }

        public InstaUserInfo(string login, string password, string[] tags, InstaUserActionType[] actions)
        {
            Login = login;
            Password = password;
            Tags = tags;
            Actions = actions;
        }
    }
}
