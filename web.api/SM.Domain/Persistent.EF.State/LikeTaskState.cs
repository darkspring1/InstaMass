using System;
namespace SM.Domain.Persistent.EF.State
{
    public class LikeTaskState
    {
        public Guid TaskId { get; set; }
        public TaskState Task { get; set; }
        public string Tags { get; set; }
    }
}
