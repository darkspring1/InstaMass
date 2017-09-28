using SM.Domain.Model;

namespace SM.Domain.Events
{
    public class LikeTaskWasCreated : IDomainEvent
    {
        public LikeTask Task { get; set; }
    }
}
