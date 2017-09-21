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
    }
}
