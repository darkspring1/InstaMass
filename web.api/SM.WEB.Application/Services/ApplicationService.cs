using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Persistent;
using System.Threading.Tasks;
using SM.Domain.Events;

namespace SM.WEB.Application.Services
{
    public class ApplicationService : SMBaseService
    {
        public ApplicationService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(unitOfWork, logger, eventDispatcher)
        {
        }

        public Task<ServiceResult<Domain.Model.Application>> GetByIdAsync(string id)
        {
            return RunAsync(() => UnitOfWork.ApplicationRepository.GetByIdAsync(id));
        }
    }
}
