namespace SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Commands
{
    /// <summary>
    /// отправляем sender'у,  в подтверждение того,, что аккайн был сохранён
    /// </summary>
    public class SyncAccountVersion
    {
        public SyncAccountVersion(string login, int version)
        {
            Login = login;
            Version = version;
        }

        public string Login { get; }
        public int Version { get; }
    }
}
