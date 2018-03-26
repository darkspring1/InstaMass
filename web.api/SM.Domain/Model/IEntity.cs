namespace SM.Domain.Model
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
