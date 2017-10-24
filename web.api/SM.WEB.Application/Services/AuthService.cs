using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System.Threading.Tasks;
using SM.Domain.Events;

namespace SM.WEB.Application.Services
{
    public class AuthService : SMBaseService
    {
        public AuthService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(unitOfWork, logger, eventDispatcher)
        {
        }

        public Task<ServiceResult> AddRefreshTokenAsync(RefreshToken token)
        {
            return RunAsync(() =>
            {
                UnitOfWork.RefreshTokenRepository.AddNewTokenAsync(token);
                return UnitOfWork.CompleteAsync();
            });
        }

        public Task<ServiceResult<RefreshToken>> FindRefreshTokenAsync(string tokenId)
        {
            return RunAsync(() => UnitOfWork.RefreshTokenRepository.GetByIdAsync(tokenId));
        }


        public Task<ServiceResult> RemoveRefreshTokenAsync(string tokenId)
        {
            return RunAsync(async () => {
                await UnitOfWork.RefreshTokenRepository.RemoveAsync(tokenId);
                await UnitOfWork.CompleteAsync();
                }
            );
        }
    }
}
