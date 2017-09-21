namespace SM.Domain.Events
{
    public interface IDomainEventDispatcher
    {
        void Raise<T>(T args) where T : IDomainEvent;
    }
}
