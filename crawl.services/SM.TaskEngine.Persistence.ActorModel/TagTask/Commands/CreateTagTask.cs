namespace SM.TaskEngine.Persistence.ActorModel.TagTask.Commands
{
    public class CreateTagTask
    {
        public string Login { get; set; }

        public string[] Tags { get; set; }
    }
}
