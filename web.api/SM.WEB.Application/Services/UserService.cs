using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System.Threading.Tasks;

namespace SM.WEB.Application.Services
{
    public class UserService : BaseService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ServiceResult> CreateAync(string email, string userName, string password)
        {
            return RunAsync(() => {
                var newUser = User.Create(email, userName, password);
                _unitOfWork.UserRepository.RegisterNewUser(newUser);
                return _unitOfWork.CompleteAsync();
            });
           
        }
    }
}
