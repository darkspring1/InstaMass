namespace SM.Domain.Events
{
    public interface ICanHandle<T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
