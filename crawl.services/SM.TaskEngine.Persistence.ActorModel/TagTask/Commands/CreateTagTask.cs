namespace SM.TaskEngine.Persistence.ActorModel.TagTask.Commands
{
    public class CreateTagTask
    {

        public int Version { get; }

        public string Login { get; }

        public string[] Tags { get; }


        public CreateTagTask(int version, string login, string[] tags)
        {
            Version = version;
            Login = login;
            Tags = tags;
        }

    }
}
