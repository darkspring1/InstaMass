using SM.Common.Services;
using SM.Common.Log;
using SM.Domain.Persistent;
using SM.Domain.Events;
using System.Threading.Tasks;

namespace SM.Domain.Services
{
    public class DomainService : BaseService
    {
        protected IUnitOfWork UnitOfWork { get; private set; }
        private readonly IDomainEventDispatcher _eventDispatcher;

        public DomainService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(logger)
        {
            UnitOfWork = unitOfWork;
            _eventDispatcher = eventDispatcher;
        }

        protected Task RaiseAsync<T>(T @event) where T : IDomainEvent
        {
            return _eventDispatcher.RaiseAsync(@event);
        }
    }
}
