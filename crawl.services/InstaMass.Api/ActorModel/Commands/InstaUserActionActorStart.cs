namespace InstaMass.Api.ActorModel.Commands
{
    public class InstaUserActionActorStart
    {

        public string InstaLogin { get; }
        public string InstaPassword { get; }
        public string[] Tags { get; }
        public InstaUserActionType[] Actions { get; }

        public InstaUserActionActorStart(string instaLogin, string instaPassword, string[] tags, InstaUserActionType[] actions)
        {
            InstaLogin = instaLogin;
            InstaPassword = instaPassword;
            Tags = tags;
            Actions = actions;
        }
    }
}
