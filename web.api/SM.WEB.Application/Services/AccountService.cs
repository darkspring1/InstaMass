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
        public const int AccountAlreadyRegistred = 1;

        private IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<Account>> CreateAync(Guid userId, string instagramLogin, string instagramPassword)
        {
            //todo: проверять, что логин и пароль верные

            var existResult = await RunAsync(() => _unitOfWork.AccountRepository.IsExistAsync(instagramLogin));
            if (existResult.IsFaulted)
            {
                return ServiceResult<Account>.Error(existResult.Exception);
            }

            if (existResult.Result)
            {
                return ServiceResult<Account>.Error(AccountAlreadyRegistred, "AccountAlreadyRegistred");
            }

            return await RunAsync(async () => {
                var newAccount = Account.Create(userId, instagramLogin, instagramPassword);
                _unitOfWork.AccountRepository.CreateNewAccount(newAccount);
                await _unitOfWork.CompleteAsync();
                return newAccount;
            });  
        }

        //public Task<ServiceResult<bool>> IsExist(string instagramLogin)
        //{
        //    return RunAsync(() => _unitOfWork.AccountRepository.IsExistAsync(instagramLogin));
        //}

        public Task<ServiceResult<Account[]>> FindByUser(Guid userId)
        {
            return RunAsync(() => _unitOfWork.AccountRepository.FindByUserAsync(userId));
        }
    }
}
