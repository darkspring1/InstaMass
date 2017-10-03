namespace SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Events
{
    class AccountUpdated
    {
        public string Login { get; }
        public string Password { get; }

        public AccountUpdated(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
