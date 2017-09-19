using SM.Domain.Persistent.EF.State;
using System;

namespace SM.Domain.Model
{
    public class LikeTask: Entity<LikeTaskState>
    {
        internal LikeTask(LikeTaskState state) : base(state)
        {
            
        }

        public static LikeTask Create(Guid accountId, string[] tags)
        {
            var tagsStr = string.Join(",", tags);
            TaskState baseTaskState = new TaskState
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                TypeId = (int)TaskTypeEnum.Like,
                CreatedAt = DateTime.UtcNow
            };

            LikeTaskState state = new LikeTaskState
            {
                Task = baseTaskState,
                Tags = tagsStr
            };

            return new LikeTask(state);
        }
    }
}
