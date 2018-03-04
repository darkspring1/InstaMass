using System;
using System.Threading.Tasks;
using SM.Common.Cache;
using SM.Domain.Model;
using SM.Domain.Persistent.EF.State;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SM.Domain.Persistent.EF
{
    class AccountRepository : BaseRepository<Account, Account>, IAccountRepository
    {
        public AccountRepository(ICacheProvider cacheProvider, IEntityFrameworkDataContext context) : base(cacheProvider, context)
        {
        }

        public void CreateNewAccount(Account account)
        {
            Set.Add(account);
        }

        public async Task<Account[]> FindByUserAsync(Guid userId)
        {
            var accountStates = await Set.Entities.Where(a => a.UserId == userId).ToArrayAsync();
            return accountStates.Select(s => Create(s)).ToArray();
        }

        Task<Account> FindByLogin(string login)
        {
            return FirstOrDefaultAsync(Set.Entities.Where(a => a.Login == login));
        }

        public async Task<bool> IsExistAsync(string login)
        {
            var state = await FindByLogin(login);
            return state != null;
        }

        protected override Account Create(Account state)
        {
            return state;
        }

        public Task<Account> GetByIdAsync(Guid accountId)
        {
            return CreateAsync(FirstAsync(Set.Entities.Where(a => a.Id == accountId)));
        }
    }
}
