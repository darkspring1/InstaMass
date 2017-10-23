namespace SM.TaskEngine.Api.ActorModel.Commands
{
    public class UpdateTagTask
    {

        public UpdateTagTask(int version, string id, string[] tags)
        {
            Version = version;
            Id = id;
            Tags = tags;
        }

        public int Version { get; }
        public string Id { get; }
        public string[] Tags { get; }
    }
}
