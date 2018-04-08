using SM.Common.Log;
using SM.Common.Services;
using SM.Domain.Model;
using SM.Domain.Persistent;
using System;
using System.Threading.Tasks;
using SM.Domain.Events;

namespace SM.Domain.Services
{

    public class AccountService : DomainService
    {
        public const int AccountAlreadyRegistred = 1;

        public AccountService(IUnitOfWork unitOfWork, ILogger logger, IDomainEventDispatcher eventDispatcher) : base(unitOfWork, logger, eventDispatcher)
        {
        }

        public async Task<ServiceResult<Account>> CreateAync(Guid userId, string instagramLogin, string instagramPassword)
        {
            //todo: проверять, что логин и пароль верные

            var existResult = await RunAsync(() => UnitOfWork.AccountRepository.IsExistAsync(instagramLogin));
            if (existResult.IsFaulted)
            {
                return existResult.CastToFault<Account>();
            }

            if (existResult.Result)
            {
                return ServiceResult.Fault<Account>(ServiceErrors.Error2());
            }

            return await RunAsync(async () => {
                var newAccount = Account.Create(userId, instagramLogin, instagramPassword);
                UnitOfWork.AccountRepository.CreateNewAccount(newAccount);
                await UnitOfWork.CompleteAsync();
                return newAccount;
            });  
        }

        //public Task<ServiceResult<bool>> IsExist(string instagramLogin)
        //{
        //    return RunAsync(() => _unitOfWork.AccountRepository.IsExistAsync(instagramLogin));
        //}

        public Task<ServiceResult<Account[]>> FindByUser(Guid userId)
        {
            return RunAsync(() => UnitOfWork.AccountRepository.FindByUserAsync(userId));
        }
    }
}
