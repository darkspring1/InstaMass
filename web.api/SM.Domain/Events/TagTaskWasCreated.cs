using SM.Domain.Model;

namespace SM.Domain.Events
{
    public class TagTaskWasCreated : IDomainEvent
    {
        public TagTaskWasCreated(string instagramAccountLogin, TagTask task)
        {
            InstagramAccountLogin = instagramAccountLogin;
            Task = task;
        }

        public string InstagramAccountLogin { get; }
        public TagTask Task { get; }
    }
}
