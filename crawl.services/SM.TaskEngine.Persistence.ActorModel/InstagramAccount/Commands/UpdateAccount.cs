namespace SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Commands
{
    public class UpdateAccount
    {

        public UpdateAccount(int version, string password)
        {
            Password = password;
            Version = version;
        }


        /// <summary>
        /// номер версии для синхронизации со внешними системами
        /// </summary>
        public int Version { get; }
        public string Password { get; }

    }
}
