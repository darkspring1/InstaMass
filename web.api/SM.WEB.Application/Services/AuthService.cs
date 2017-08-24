using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System.Threading.Tasks;

namespace SM.WEB.Application.Services
{
    public class AuthService : BaseService
    {
        private IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ServiceResult> AddRefreshTokenAsync(RefreshToken token)
        {
            return RunAsync(() =>
            {
                _unitOfWork.RefreshTokenRepository.AddNewTokenAsync(token);
                return _unitOfWork.CompleteAsync();
            });
        }

        public Task<ServiceResult<RefreshToken>> FindRefreshTokenAsync(string tokenId)
        {
            return RunAsync(() => _unitOfWork.RefreshTokenRepository.GetByIdAsync(tokenId));
        }


        public Task<ServiceResult> RemoveRefreshTokenAsync(string tokenId)
        {
            return RunAsync(async () => {
                await _unitOfWork.RefreshTokenRepository.RemoveAsync(tokenId);
                await _unitOfWork.CompleteAsync();
                }
            );
        }
    }
}
