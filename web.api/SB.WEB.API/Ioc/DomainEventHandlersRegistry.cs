using SM.Domain.Events;
using SM.Domain.Events.Sync;
using SM.WEB.Application.DomainEventHandlers;
using StructureMap;
namespace SM.WEB.API.Ioc
{
    public class DomainEventHandlersRegistry : Registry
    {
        public DomainEventHandlersRegistry()
        {
            For<ICanHandle<TagTaskWasCreated>>().Use<TagTaskCreatedHandler>();
            For<ICanHandle<TagTaskWasSyncWithExternalSystem>>().Use<TagTaskSyncHandler>();
        }
    }
}
