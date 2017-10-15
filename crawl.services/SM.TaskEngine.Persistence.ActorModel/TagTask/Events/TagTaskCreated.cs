namespace SM.TaskEngine.Persistence.ActorModel.TagTask.Events
{
    public class TagTaskCreated
    {
        public string Login { get; set; }

        public string [] Tags { get; set; }
    }
}
