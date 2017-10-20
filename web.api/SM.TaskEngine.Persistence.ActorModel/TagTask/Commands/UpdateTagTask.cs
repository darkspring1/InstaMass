namespace SM.TaskEngine.Persistence.ActorModel.TagTask.Commands
{
    public class UpdateTagTask
    {
        public UpdateTagTask(int version, string[] tags)
        {
            Version = version;
            Tags = tags;
        }

        public int Version { get; }
        public string[] Tags { get; }
    }
}
