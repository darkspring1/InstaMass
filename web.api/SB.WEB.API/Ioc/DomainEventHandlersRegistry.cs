using Akka.Actor;
using SM.Domain.Events;
using SM.Domain.Events.Sync;
using SM.WEB.Application.DomainEventHandlers;
using StructureMap;
namespace SM.WEB.API.Ioc
{
    public class DomainEventHandlersRegistry : Registry
    {
        public DomainEventHandlersRegistry(IActorRef taskApi)
        {
            For<ICanHandle<TagTaskWasCreated>>()
                .Use<TagTaskCreatedHandler>()
                .Ctor<IActorRef>()
                .Is(taskApi);

            For<ICanHandle<TagTaskWasSyncWithExternalSystem>>().Use<TagTaskSyncHandler>();
        }
    }
}
