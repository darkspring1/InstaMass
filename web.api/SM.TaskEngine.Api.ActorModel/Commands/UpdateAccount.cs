namespace SM.TaskEngine.Api.ActorModel.Commands
{
    public class UpdateAccount
    {

        public UpdateAccount(string login, string password, int version)
        {
            Login = login;
            Password = password;
            Version = version;
        }

        public string Login { get; }
        public string Password { get; }
        public int Version { get; }
    }
}
