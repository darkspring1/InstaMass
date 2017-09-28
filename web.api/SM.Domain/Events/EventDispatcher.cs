using System.Linq;
using System.Threading.Tasks;

namespace SM.Domain.Events
{
    public class EventDispatcher : IDomainEventDispatcher
    {
        IHandlerContainer _container;
        public EventDispatcher(IHandlerContainer container)
        {
            _container = container;
        }

        public void Raise<T>(T @event) where T : IDomainEvent
        {
            var handlers = _container.ResolveAll<T>();
            foreach (var h in handlers)
            {
                h.Handle(@event);
            }
        }

        public Task RaiseAsync<T>(T args) where T : IDomainEvent
        {
            var handlers = _container.ResolveAll<T>();
            var tasks = handlers.Select(h => h.HandleAsync(args));
            return Task.WhenAll(tasks);
        }
    }
}
