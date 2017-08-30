using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System;
using System.Threading.Tasks;

namespace SM.WEB.Application.Services
{
    public class AccountService : BaseService
    {
        private IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ServiceResult<Account>> CreateAync(Guid userId, string instagramLogin, string instagramPassword)
        {
            return RunAsync(async () => {
                var newAccount = Account.Create(userId, instagramLogin, instagramPassword);
                _unitOfWork.AccountRepository.CreateNewAccount(newAccount);
                await _unitOfWork.CompleteAsync();
                return newAccount;
            });  
        }

        public Task<ServiceResult<Account[]>> FindByUser(Guid userId)
        {
            return RunAsync(() => _unitOfWork.AccountRepository.FindByUserAsync(userId));
        }
    }
}
