using SM.Domain.Events;
using StructureMap;
using System.Collections.Generic;

namespace SM.WEB.API.Ioc
{
    class StructureMapEventHandlerContainer : IHandlerContainer
    {
        IContainer _container;
        public StructureMapEventHandlerContainer(IContainer container)
        {
            _container = container;
        }

        public IEnumerable<ICanHandle<T>> ResolveAll<T>() where T : IDomainEvent
        {
            return _container.GetAllInstances<ICanHandle<T>>();
        }
    }
}
