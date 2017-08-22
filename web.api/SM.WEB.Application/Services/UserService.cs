using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using SM.Domain.Services;
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

        public Task<ServiceResult> CreateExternalAsync(ExternalUserInfo userInfo)
        {
            return RunAsync(() => {
                var newUser = User.CreateExternal(userInfo);
                _unitOfWork.UserRepository.RegisterNewUser(newUser);
                return _unitOfWork.CompleteAsync();
            });
        }

        public Task<ServiceResult<User>> FindAsync(ExternalAuthProviderType providerType, string externalUserId)
        {
            return RunAsync(() => _unitOfWork.UserRepository.FindAsync(providerType, externalUserId));
        }

        public Task<ServiceResult<ExternalUserInfo>> GetExternalUserInfoAsync(ExternalAuthProviderType providerType, string accessToken)
        {
            return RunAsync(() => User.GetExternalUserInfoAsync(providerType, accessToken));
        }
    }
}
