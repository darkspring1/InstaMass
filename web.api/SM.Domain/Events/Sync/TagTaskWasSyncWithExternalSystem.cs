namespace SM.Domain.Events.Sync
{
    public class TagTaskWasSyncWithExternalSystem : IDomainEvent
    {
        public TagTaskWasSyncWithExternalSystem(int externalSystemVersionVersion, string taskId)
        {
            ExternalSystemVersion = externalSystemVersionVersion;
            TaskId = taskId;
        }

        public int ExternalSystemVersion { get; }
        public string TaskId { get; }
    }
}
