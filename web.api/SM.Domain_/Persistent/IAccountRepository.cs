using SM.Domain.Model;
using System;
using System.Threading.Tasks;

namespace SM.Domain.Persistent
{
    public interface IAccountRepository
    {
        void CreateNewAccount(Account account);

        Task<Account[]> FindByUserAsync(Guid userId);

        Task<Account> GetByIdAsync(Guid accountId);

        Task<bool> IsExistAsync(string login);
    }
}