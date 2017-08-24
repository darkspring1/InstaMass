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

        public Task<ServiceResult<User>> CreateAync(string email, string userName, string password)
        {
            return RunAsync(async () => {
                var newUser = User.Create(email, userName, password);
                _unitOfWork.UserRepository.RegisterNewUser(newUser);
                await _unitOfWork.CompleteAsync();
                return newUser;
            });  
        }

        public Task<ServiceResult<User>> CreateExternalAsync(ExternalUserInfo userInfo)
        {
            return RunAsync(async () => {
                var newUser = User.CreateExternal(userInfo);
                _unitOfWork.UserRepository.RegisterNewUser(newUser);
                await _unitOfWork.CompleteAsync();
                return newUser;
            });
        }

        public Task<ServiceResult<User>> FindAsync(ExternalAuthProviderType providerType, string externalUserId)
        {
            return RunAsync(() => _unitOfWork.UserRepository.FindAsync(providerType, externalUserId));
        }

        public Task<ServiceResult<User>> FindAsync(string email, string password)
        {
            return RunAsync(() => _unitOfWork.UserRepository.FindAsync(email, password));
        }

        public Task<ServiceResult<ExternalUserInfo>> GetExternalUserInfoAsync(ExternalAuthProviderType providerType, string accessToken)
        {
            return RunAsync(() => User.GetExternalUserInfoAsync(providerType, accessToken));
        }
    }
}
