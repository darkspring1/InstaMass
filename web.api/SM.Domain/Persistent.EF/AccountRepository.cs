using System;
using System.Threading.Tasks;
using SM.Common.Cache;
using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System.Linq;
using System.Data.Entity;

namespace SM.Domain.Persistent.EF
{
    class AccountRepository : BaseRepository<Account, AccountState>, IAccountRepository
    {
        public AccountRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        public void CreateNewAccount(Account account)
        {
            Set.Add(account.State);
        }

        public async Task<Account[]> FindByUserAsync(Guid userId)
        {
            var accountStates = await Set.Entities.Where(a => a.UserId == userId).ToArrayAsync();
            return accountStates.Select(s => Create(s)).ToArray();
        }

        Task<AccountState> FindByLogin(string login)
        {
            return FirstOrDefaultAsync(Set.Entities.Where(a => a.Login == login));
        }

        public async Task<bool> IsExistAsync(string login)
        {
            var state = await FindByLogin(login);
            return state != null;
        }

        protected override Account Create(AccountState state)
        {
            return new Account(state);
        }
    }
}
