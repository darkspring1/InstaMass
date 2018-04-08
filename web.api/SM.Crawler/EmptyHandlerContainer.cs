using SM.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SM.Crawler
{
    class EmptyHandlerContainer : IHandlerContainer
    {
        public IEnumerable<ICanHandle<T>> ResolveAll<T>() where T : IDomainEvent
        {
            throw new NotImplementedException();
        }
    }
}
