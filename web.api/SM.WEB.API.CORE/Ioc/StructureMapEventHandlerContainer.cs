using SM.Domain.Events;
using StructureMap;
using System;
using System.Collections.Generic;

namespace SM.WEB.API.Ioc
{
    class StructureMapEventHandlerContainer : IHandlerContainer, IDisposable
    {
        IContainer _container;
        public StructureMapEventHandlerContainer(IContainer container)
        {
            _container = container.GetNestedContainer();
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public IEnumerable<ICanHandle<T>> ResolveAll<T>() where T : IDomainEvent
        {
            return _container.GetAllInstances<ICanHandle<T>>();
        }
    }
}
