using System;

namespace InstaMass
{

    public enum InstaUserActionType
    {
        LikeByTag = 0,
        LikeByLocation = 1,
        Sunscribe = 2
    }
    public class InstaAction
    {
        public InstaUserActionType Type  { get; }

        public DateTime ExecutedAt { get; }

        public InstaAction(InstaUserActionType type, DateTime executedAt)
        {
            Type = type;
            ExecutedAt = executedAt;
        }
    }


    class LikeByTagAction : InstaAction
    {
        public string MediaId { get; }
        public LikeByTagAction(string mediaId, DateTime executedAt)
            : base(InstaUserActionType.LikeByTag, executedAt)
        {
            MediaId = mediaId;
        }
    }
}
