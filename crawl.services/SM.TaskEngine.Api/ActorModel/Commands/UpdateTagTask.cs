namespace SM.TaskEngine.Api.ActorModel.Commands
{
    public class UpdateTagTask
    {
        public string Id { get; set; }

        public string[] Tags { get; set; }
    }
}
