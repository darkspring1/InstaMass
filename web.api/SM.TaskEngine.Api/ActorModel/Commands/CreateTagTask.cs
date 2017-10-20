namespace SM.TaskEngine.Api.ActorModel.Commands
{
    public class CreateTagTask
    {

        public CreateTagTask(int version, string id, string login, string[] tags)
        {
            Version = version;
            Id = id;
            Login = login;
            Tags = tags;
        }

        public int Version { get; }
        public string Id { get; }
        public string Login { get; }
        public string [] Tags { get; }
    }
}
