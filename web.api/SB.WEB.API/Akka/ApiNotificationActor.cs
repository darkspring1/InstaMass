using Akka.Actor;
using SM.Domain.Events;
using SM.Domain.Events.Sync;
using SM.TaskEngine.Persistence.ActorModel.InstagramAccount.Commands;

namespace SM.WEB.API.Akka
{
    public class ApiNotificationActor : ReceiveActor
    {
        public ApiNotificationActor(IDomainEventDispatcher eventDispatcher)
        {
            Receive<SyncTagTaskVersion>(m =>
            {
                var domainEvent = new TagTaskWasSyncWithExternalSystem(m.Version, m.Id);
                eventDispatcher.Raise(domainEvent);
            });
        }
    }
}
