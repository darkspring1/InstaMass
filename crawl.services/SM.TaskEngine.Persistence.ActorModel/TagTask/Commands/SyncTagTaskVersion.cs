namespace SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Commands
{
    /// <summary>
    /// отправляем sender'у,  в подтверждение того,, что аккайн был сохранён
    /// </summary>
    public class SyncTagTaskVersion
    {
        public SyncTagTaskVersion(int version, string id)
        {
            Version = version;
            Id = id;
        }

        public int Version { get; }
        public string Id { get; }
        
    }
}
