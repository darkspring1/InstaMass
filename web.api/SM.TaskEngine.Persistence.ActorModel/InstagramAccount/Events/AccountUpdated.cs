namespace SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Events
{
    class AccountUpdated
    {
        public string Password { get; }

        public AccountUpdated(string password)
        {
            Password = password;
        }
    }
}
