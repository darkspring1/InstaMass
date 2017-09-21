using System.Collections.Generic;

namespace SM.Domain.Events
{
    public interface IHandlerContainer
    {
        IEnumerable<ICanHandle<T>> ResolveAll<T>() where T : IDomainEvent;
    }
}
