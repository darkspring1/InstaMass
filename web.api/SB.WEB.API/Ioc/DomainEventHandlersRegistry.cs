using SM.Domain.Events;
using SM.WEB.Application.DomainEventHandlers;
using StructureMap;
namespace SM.WEB.API.Ioc
{
    public class DomainEventHandlersRegistry : Registry
    {
        public DomainEventHandlersRegistry()
        {
            For<IDomainEventDispatcher>().Use<EventDispatcher>();
            For<ICanHandle<LikeTaskWasCreated>>().Use<LikeTaskCreateHandler>();
        }
    }
}
