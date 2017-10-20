using SM.Domain.Model;

namespace SM.Domain.Events
{
    public class TagTaskWasCreated : IDomainEvent
    {
        public TagTask Task { get; set; }
    }
}
