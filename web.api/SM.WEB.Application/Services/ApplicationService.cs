using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Persistent;
using System.Threading.Tasks;

namespace SM.WEB.Application.Services
{
    public class ApplicationService : BaseService
    {
        private IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<SM.Domain.Model.Application> GetByIdAsync(string id)
        {
            return _unitOfWork.ApplicationRepository.GetByIdAsync(id);
        }
    }
}
